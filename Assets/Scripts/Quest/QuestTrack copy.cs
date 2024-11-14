using UnityEngine;

public class QuestChangerOnRaycast : MonoBehaviour
{
    public float detectionDistance = 5f; // Distance to check for the "Server" object

    void Update()
    {
        // Perform a raycast forward from the object's position
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionDistance))
        {
            // Check if the hit object is tagged as "Server"
            if (hit.collider.CompareTag("Server"))
            {
                // Change the quest to 5 in PlayerPrefs
                PlayerPrefs.SetInt("quest", 5);
                PlayerPrefs.Save();
                Debug.Log("Quest changed to 5 due to proximity to Server object.");

                // Optionally disable this script after the quest change
                this.enabled = false; // Disables the script after the first detection
            }
        }
    }

    // Optional: Visualize the raycast in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * detectionDistance);
    }
}
