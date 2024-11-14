using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject player; // Reference to the player
    public Transform holdPos; // Position where the item will be held
    public InventoryManager inventoryManager; // Reference to the InventoryManager
    public float throwForce = 7F; // Force at which the object is thrown
    public float pickUpRange = 2f; // Distance within which the object can be picked up
    private float rotationSensitivity = 1f; // Sensitivity for object rotation
    private GameObject heldObj; // Object that is currently held
    private Rigidbody heldObjRb; // Rigidbody of the object being held
    private bool canDrop = true; // Flag to check if the object can be dropped
    private int LayerNumber; // Layer index for holding the object

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");
    }

    void Update()
    {
        // Check for pickup/drop input from both keyboard and controller
        if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("ControllerX")) // Make sure "ControllerX" is defined in Input Manager
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    if (hit.transform.gameObject.CompareTag("canPickUp") || hit.transform.gameObject.CompareTag("badge"))
                    {
                        PickUpObject(hit.transform.gameObject);
                    }
                }
            }
            else
            {
                if (canDrop)
                {
                    StopClipping();
                    DropObject();
                }
            }
        }

        // Check for inventory input
        if (Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("ControllerY"))
        {
            if (heldObj != null)
            {
                inventoryManager.PickUpItem(heldObj);
                DropObject();
            }
        }

        if (heldObj != null)
        {
            MoveObject();
            RotateObject();


        }
    }

  public  void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObj.transform.parent = holdPos.transform;
            heldObj.layer = LayerNumber;
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
 PlayerPrefs.SetInt("pickedUp", 1);
 PlayerPrefs.Save();
            // Check if the object is the badge
            if (heldObj.CompareTag("badge"))
            {
                PlayerPrefs.SetInt("prefcard", 1);
                Debug.Log("Badge picked up, prefcard set to 1.");
            }
        }
    }

    void DropObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
 PlayerPrefs.SetInt("pickedUp", 0);
 PlayerPrefs.Save();
        // Check if the object is the badge
        if (heldObj.CompareTag("badge"))
        {
            PlayerPrefs.SetInt("prefcard", 0);
            Debug.Log("Badge dropped, prefcard set to 0.");
        }

        heldObj = null;
    }

    void MoveObject()
    {
        heldObj.transform.position = holdPos.transform.position;
    }

    void RotateObject()
    {
        if (Input.GetKey(KeyCode.R))
        {
            canDrop = false;

            if (Input.GetKey(KeyCode.I))
            {
                heldObj.transform.Rotate(Vector3.right, -rotationSensitivity);
            }
            if (Input.GetKey(KeyCode.J))
            {
                heldObj.transform.Rotate(Vector3.right, rotationSensitivity);
            }
            if (Input.GetKey(KeyCode.O))
            {
                heldObj.transform.Rotate(Vector3.down, rotationSensitivity);
            }
            if (Input.GetKey(KeyCode.U))
            {
                heldObj.transform.Rotate(Vector3.down, -rotationSensitivity);
            }
        }
        else
        {
            canDrop = true;
        }
    }

    void ThrowObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);

        // Check if the object is the badge when thrown
        if (heldObj.CompareTag("badge"))
        {
            PlayerPrefs.SetInt("prefcard", 0);
            Debug.Log("Badge thrown, prefcard set to 0.");
        }

        heldObj = null;
    }

    void StopClipping()
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);

        if (hits.Length > 1)
        {
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }
}
