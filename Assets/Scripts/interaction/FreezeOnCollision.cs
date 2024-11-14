using UnityEngine;

public class FreezeOnCollision : MonoBehaviour
{
    private Rigidbody rb;
    private int collisionCount = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Increment the collision count
        collisionCount++;

        // Freeze the Rigidbody if it's not already frozen
        if (rb != null && collisionCount == 1)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Debug.Log("Rigidbody has been frozen on collision.");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Decrement the collision count
        collisionCount--;

        // Unfreeze the Rigidbody if no collisions are detected
        if (collisionCount <= 0 && rb != null)
        {
            rb.constraints = RigidbodyConstraints.None; // Unfreeze all constraints
            Debug.Log("Rigidbody has been unfrozen.");
        }
    }
}
