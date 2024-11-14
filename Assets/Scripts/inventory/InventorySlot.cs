using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemImage; // Image component for displaying the item
    public Button slotButton; // Button component for click handling

    private GameObject item; // The item associated with this slot
    private InventoryManager inventoryManager;
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f; // Time in seconds to consider as a double click

    public void Setup(GameObject item, InventoryManager inventoryManager)
    {
        // Check for null references
        if (itemImage == null || slotButton == null)
        {
            Debug.LogError("ItemImage or SlotButton is not assigned in InventorySlot.");
            return;
        }

        if (item == null || inventoryManager == null)
        {
            Debug.LogError("Item or InventoryManager is not assigned.");
            return;
        }

        // Initialize item and inventory manager
        this.item = item;
        this.inventoryManager = inventoryManager;

        // Set the item's sprite
        Sprite itemSprite = inventoryManager.GetItemSprite(item);
        itemImage.sprite = itemSprite ?? null; // Set or clear the item's sprite

        // Add listener for button click
        slotButton.onClick.RemoveAllListeners(); // Clear existing listeners
        slotButton.onClick.AddListener(OnSlotClick);
    }

    private void OnSlotClick()
    {
        float currentTime = Time.time;
        // Check for double click
        if (currentTime - lastClickTime < doubleClickThreshold)
        {
            OnDoubleClick();
        }
        lastClickTime = currentTime; // Update the last click time
    }

    private void OnDoubleClick()
    {
        Debug.Log("Slot double-clicked");
int check = PlayerPrefs.GetInt("pickedUp");
        if (item == null || inventoryManager == null)
        {
            Debug.LogError("Item or InventoryManager is not assigned.");
            return;
        }
      if (check==1)
        {
            Debug.LogError("drop the item first ");
            return;

        }
        // Spawn the item in the world as if it's picked up
        GameObject itemPrefab = inventoryManager.GetItemPrefab(item);
        if (itemPrefab != null)
        {
            Debug.Log("Spawning item: " + itemPrefab.name);
            GameObject instantiatedItem = Instantiate(itemPrefab, inventoryManager.holdPos.position, Quaternion.identity);
            instantiatedItem.SetActive(true);

            // Handle the item as if it's picked up
            inventoryManager.PickUpItem(instantiatedItem);
            inventoryManager.currentItem = null;
        }
        else
        {
            Debug.LogError("Item prefab not found.");
        }

        // Remove the slot from the UI
        Destroy(gameObject);
        if (inventoryManager.inventoryItems.Contains(item))
        {
            inventoryManager.inventoryItems.Remove(item);
            Debug.Log("Item removed from inventory: " + item.name);
        }
        else
        {
            Debug.LogError("Item not found in the inventory list.");
            return;
        }
    }

    // Call this method in the Update function of a MonoBehaviour that handles input
    public void Update()
    {
        if (Input.GetButtonDown("R2")) // Ensure "R2" is defined in Input Manager
        {
            OnR2Click();
        }
    }

    private void OnR2Click()
    {
         Debug.Log("Slot double-clicked");

        if (item == null || inventoryManager == null)
        {
            Debug.LogError("Item or InventoryManager is not assigned.");
            return;
        }

        // Spawn the item in the world as if it's picked up
        GameObject itemPrefab = inventoryManager.GetItemPrefab(item);
        if (itemPrefab != null)
        {
            Debug.Log("Spawning item: " + itemPrefab.name);
            GameObject instantiatedItem = Instantiate(itemPrefab, inventoryManager.holdPos.position, Quaternion.identity);
            instantiatedItem.SetActive(true);

            // Handle the item as if it's picked up
            inventoryManager.PickUpItem(instantiatedItem);
            inventoryManager.currentItem = null;
        }
        else
        {
            Debug.LogError("Item prefab not found.");
        }

        // Remove the slot from the UI
        Destroy(gameObject);
        if (inventoryManager.inventoryItems.Contains(item))
        {
            inventoryManager.inventoryItems.Remove(item);
            Debug.Log("Item removed from inventory: " + item.name);
        }
        else
        {
            Debug.LogError("Item not found in the inventory list.");
            return;
        }
        }

}
