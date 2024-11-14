using UnityEngine;

public class DesktopCollisionHandler : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
    }

    // This method is called when another object enters the trigger collider attached to this GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has the tag "laptop"
        if (other.gameObject.CompareTag("laptop"))
        {
            // Log the trigger event
            Debug.Log("Triggered by the target object (laptop)");
            
            // Freeze all Rigidbody constraints to stop the movement
            rb.constraints = RigidbodyConstraints.FreezeAll;

            // Perform any other desired actions when the trigger occurs
            Debug.Log("Yey Copy meeee");

            // Set the PlayerPrefs variable "DataCopy" to 1
            PlayerPrefs.SetInt("DataCopy", 1);
            PlayerPrefs.Save();

            // Log the successful actions
            Debug.Log("Object set to inactive, DataCopy set to 1.");
        }
    }

    // This method is called when the object leaves the trigger collider attached to this GameObject
    private void OnTriggerExit(Collider other)
    {
        // Unfreeze the Rigidbody constraints to allow movement again
        rb.constraints = RigidbodyConstraints.None;
        
        // Log the exit event
        Debug.Log("Exited trigger with the target object (laptop)");
    }
}
