using UnityEngine;

public class QuestCheckAndTag : MonoBehaviour
{
    // The name of the tag to switch to
    public string interactableTag = "Interactable";

    // The Raycast Interactable component that you want to enable
    private RaycastInteractable raycastInteractable;

    void Start()
    {
        // Get the Raycast Interactable component attached to the same GameObject
        raycastInteractable = GetComponent<RaycastInteractable>();

        if (raycastInteractable == null)
        {
            Debug.LogError("RaycastInteractable component not found on this GameObject.");
        }
    }

    void Update()
    {
        // Continuously check if the quest value is 6
        if (PlayerPrefs.GetInt("quest", 0) == 6)
        {
            // Change the tag to "interactable"
            gameObject.tag = interactableTag;

            // Activate the Raycast Interactable component if it exists
            if (raycastInteractable != null)
            {
                raycastInteractable.enabled = true;
            }
        }
    }
}
