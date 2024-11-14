using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventorySlotPrefab; // Prefab for inventory slots
    public Transform inventoryPanel; // Parent UI panel for inventory slots
    public Transform holdPos; // Position where the item will be held
    public PickUpScript pickup;
    public GameObject currentItem; // Currently picked up item
    public List<GameObject> inventoryItems = new List<GameObject>(); // List to store items
    public GameObject firstItemNotification; // GameObject to appear on first item addition
    public GameObject checkednotfication; // GameObject to appear on first item addition
    
    private int inventorycount=0;

    private bool firstItemAdded = false; // Track if the first item has been added

    void Start()
    {
        PlayerPrefs.SetInt("server_pc_opened", 0);
        PlayerPrefs.Save(); // Save the PlayerPrefs
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("ControllerY"))
        {
            AddToInventory();
            inventorycount+=1;

        }

        // Check if R2 button is pressed
        if (Input.GetButtonDown("R2")) // Ensure "R2" is defined in Input Manager
        {
            UseAllItems(); // Call method to use all items
        }
    }

    public void PickUpItem(GameObject item)
    {
        currentItem = item;
        pickup.PickUpObject(currentItem);
    }

public void AddToInventory()
{
    if (currentItem != null)
    {
        // Check if inventory is full (max 3 items)
        if (inventoryItems.Count >= 2)
        {
            Debug.Log("Inventory is full. Can't add more items.");
            return;
        }

        Debug.Log("Adding item to inventory: " + currentItem.name); // Log item name

        // Check if the item is a smartphone
        if (currentItem.name.ToLower().Contains("smartphone"))
        {
            Debug.Log("yey smartphone");
        }

        // Calculate the position for the new slot
        Vector3 newSlotPosition;
        if (inventoryItems.Count == 0) // No items yet, start at the inventory panel position
        {
            newSlotPosition = inventoryPanel.position;
        }
        else // There are already items
        {
            // Get the last inventory slot's position
            Transform lastSlotTransform = inventoryPanel.GetChild(inventoryItems.Count - 1);
            Vector3 lastSlotPosition = lastSlotTransform.position;

            // Check if the current count of items is even or odd
            if (inventorycount % 2 == 0) // Even count
            {
                newSlotPosition = lastSlotPosition; // Start at the last slot's position
                newSlotPosition.y += 65; // Offset it by 65 units above the last slot
            }
            else // Odd count
            {
                newSlotPosition = lastSlotPosition; // Start at the last slot's position
                newSlotPosition.y -= 65; // Offset it by 65 units below the last slot
            }
        }

        // Create the new slot at the calculated position
        GameObject newSlot = Instantiate(inventorySlotPrefab, newSlotPosition, Quaternion.identity, inventoryPanel);
        InventorySlot slot = newSlot.GetComponent<InventorySlot>();
        slot.Setup(currentItem, this);

        currentItem.SetActive(false); // Hide the item if you plan to reactivate it later

        // Add the item to the inventory list
        if (!inventoryItems.Contains(currentItem))
        {
            inventoryItems.Add(currentItem); // Add to inventory list

            // Check if this is the first item being added
            if (!firstItemAdded)
            {
                firstItemAdded = true; // Mark that the first item has been added
                ShowFirstItemNotification(); // Show the notification GameObject
            }
        }

        // Clear currentItem and prepare for new pickups
        currentItem = null;
    }
}



    private void ShowFirstItemNotification()
    {
        if (firstItemNotification != null)
        {
            firstItemNotification.SetActive(true); // Show the notification
            StartCoroutine(HideFirstItemNotificationAfterDelay(6f)); // Start the coroutine to hide after 2 seconds
        }
        else
        {
            Debug.LogWarning("First item notification GameObject is not assigned.");
        }
    }

    private System.Collections.IEnumerator HideFirstItemNotificationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        firstItemNotification.SetActive(false); // Hide the notification
    }

   public void UseItem(GameObject itemPrefab)
    {
        int checkedValue = PlayerPrefs.GetInt("pickeUp");

        if (checkedValue == 1)
        {
            // Activate the notification and start the coroutine to hide it
            checkednotfication.SetActive(true);
            StartCoroutine(HideNotificationAfterDelay(2f)); // Call coroutine
            return;
        }

        if (itemPrefab != null)
        {
            Debug.Log("Spawning item: " + itemPrefab.name);
            GameObject instantiatedItem = Instantiate(itemPrefab, holdPos.position, Quaternion.identity);
            instantiatedItem.SetActive(true);

            // Enable Rigidbody and freeze it for later checking
            HandleRigidbody(instantiatedItem);

            // Debugging Collider
            Collider collider = instantiatedItem.GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = true;
                Debug.Log("Item Collider enabled.");
            }
            else
            {
                Debug.LogWarning("Item does not have a Collider.");
            }
        }
        else
        {
            Debug.LogError("Item prefab is null.");
        }
    }

    private IEnumerator HideNotificationAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);
        
        // Deactivate the notification
        checkednotfication.SetActive(false);
        Debug.Log("Notification disabled after " + delay + " seconds.");
    }

    public void UseAllItems()
    {
        Debug.Log("Using all items in the inventory.");

        // Iterate through all items in the inventory and use them
        foreach (var item in inventoryItems)
        {
            if (item != null) // Check if the item is not null
            {
                Debug.Log("Spawning item: " + item.name);
                GameObject instantiatedItem = Instantiate(item, holdPos.position, Quaternion.identity);
                instantiatedItem.SetActive(true);

                // Enable Rigidbody and freeze it for later checking
                HandleRigidbody(instantiatedItem);

                // Debugging Collider
                Collider collider = instantiatedItem.GetComponent<Collider>();
                if (collider != null)
                {
                    Debug.Log("Item Collider enabled.");
                }
                else
                {
                    Debug.LogWarning("Item does not have a Collider.");
                }
            }
            else
            {
                Debug.LogError("Item is null.");
            }
        }

        // Clear the inventory after using all items
        inventoryItems.Clear();
        Debug.Log("All items used and removed from inventory.");
    }

    private void HandleRigidbody(GameObject instantiatedItem)
    {
        Rigidbody rb = instantiatedItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log("Item has Rigidbody.");
            // Freeze position on all axes for now
            rb.constraints = RigidbodyConstraints.FreezePosition; // Start frozen

            // Start a coroutine to check for collisions
            StartCoroutine(CheckForFloorCollision(instantiatedItem, rb));
        }
        else
        {
            Debug.LogWarning("Item does not have a Rigidbody.");
        }
    }

    private System.Collections.IEnumerator CheckForFloorCollision(GameObject item, Rigidbody rb)
    {
        while (true) // Run indefinitely, but could be improved to exit under certain conditions
        {
            yield return new WaitForFixedUpdate(); // Wait for the next physics update

            // Check if the item is colliding with the floor
            if (Physics.Raycast(item.transform.position, Vector3.down, out RaycastHit hit, 1f))
            {
                if (hit.collider.CompareTag("Floor")) // Check if the hit object is the floor
                {
                    // Freeze all movement and rotation when hitting the floor
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    Debug.Log("Item has collided with the floor and is now frozen.");
                    break; // Exit the coroutine if frozen
                }
                else
                {
                    // If not on the floor, you can unfreeze or let it move
                    rb.constraints = RigidbodyConstraints.None;
                }
            }
        }
    }

    public Sprite GetItemSprite(GameObject item)
    {
        SpriteRenderer spriteRenderer = item.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            return spriteRenderer.sprite;
        }

        Renderer renderer = item.GetComponent<Renderer>();
        if (renderer != null && renderer.material.mainTexture != null)
        {
            Texture2D texture = renderer.material.mainTexture as Texture2D;
            if (texture != null)
            {
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }

        return null;
    }

    public GameObject GetItemPrefab(GameObject item)
    {
        foreach (var inventoryItem in inventoryItems)
        {
            if (inventoryItem.name == item.name)
            {
                return inventoryItem;
            }
        }
        return null;
    }
}
