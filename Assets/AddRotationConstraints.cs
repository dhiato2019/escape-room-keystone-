using UnityEngine;

public class AddRotationConstraints : MonoBehaviour
{
    // Reference to the Rigidbody component
    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the GameObject
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found. Attach a Rigidbody to this GameObject.");
        }
    }

    void Update()
    {
        // Check for horizontal and vertical input
        bool isMoving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0;

        if (isMoving)
        {
            // Remove rotation constraints only for the x and z axes
            rb.constraints = RigidbodyConstraints.FreezeRotationY;
        }
        else
        {
            // Apply rotation constraints on x, y, and z when there is no input
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}
