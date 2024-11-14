using UnityEngine;

public class EnableRaycastOnQuest : MonoBehaviour
{
    // Reference to the RaycastInteractable component
    public RaycastInteractable raycastInteractable;

    void Start()
    {
        // Get the RaycastInteractable component from the GameObject

        // Ensure the component exists and start with it disabled
        if (raycastInteractable != null)
        {
            raycastInteractable.enabled = false;
        }
        else
        {
            Debug.LogWarning("RaycastInteractable component not found on this GameObject.");
        }
    }

    void Update()
    {
        // Check if the PlayerPref 'quest' has reached the value of 6
        if (PlayerPrefs.GetInt("quest", 0) == 6)
        {
            // Enable the RaycastInteractable component if the quest value is 6
            if (raycastInteractable != null && !raycastInteractable.enabled)
            {
                raycastInteractable.enabled = true;
                Debug.Log("RaycastInteractable component enabled.");
            }
        }
    }
}
