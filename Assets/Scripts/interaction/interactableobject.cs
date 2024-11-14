using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteractable : MonoBehaviour
{
    [SerializeField] private Camera playerCamera; // Assign your player camera in the inspector
    [SerializeField] private float rayDistance = 5f; // Distance for the raycast
    [SerializeField] private float interactionDistance = 5f; // Distance for interaction
    [SerializeField] private float deactivateDistance = 2f; // Distance to deactivate the object
    [SerializeField] private KeyCode interactionKey = KeyCode.F; // Key to press for interaction
    [SerializeField] private GameObject targetObject; // Assign the GameObject to toggle active state in the inspector

    [SerializeField] private string uniqueID; // Unique ID for this interactable object

    private bool isActivated = false; // Tracks if the object is currently activated
    public FirstPersonController fpc;           // Reference to FirstPersonController (assign in Inspector)

    private void Start()
    {
        // Assign a unique ID if not already set
        if (string.IsNullOrEmpty(uniqueID))
        {
            uniqueID = System.Guid.NewGuid().ToString(); // Generate a new unique ID
        }
    }

    private void Update()
    {
        // Check if the player is within deactivate distance
        if (isActivated && Vector3.Distance(playerCamera.transform.position, transform.position) > deactivateDistance)
        {
            // Deactivate the assigned target object
            ToggleActiveState(targetObject, false);
            fpc.lockCursor = true;  // Set to true when unpaused


            isActivated = false;
        }

        if (!isActivated) // Check for activation using raycast if not already activated
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            // Check if the ray hits an object within the specified distance
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                // Check if the raycast hit this specific object
                if (hit.collider.gameObject == gameObject)
                {
                    // Check if the player presses the interaction key and the object is within distance
                    if (Input.GetKeyDown(interactionKey) /*|| Input.GetButtonDown("ControllerA")*/&& Vector3.Distance(playerCamera.transform.position, hit.point) <= interactionDistance)
                    {
                        // Activate the assigned target object
                        ToggleActiveState(targetObject, true);
                        fpc.lockCursor = false;  // Set to true when unpaused

                        isActivated = true;
                    }
                }
            }
        }
        else // If already activated, allow deactivation without raycast
        {
            if (Input.GetKeyDown(interactionKey)/*||Input.GetButtonDown("ControllerA")*/)
            {
                // Deactivate the assigned target object
                ToggleActiveState(targetObject, false);
                fpc.lockCursor = true;  // Set to true when unpaused


                isActivated = false;
            }
        }
    }

    private void ToggleActiveState(GameObject obj, bool state)
    {
        if (obj != null)
        {
            // Set the active state of the GameObject
            obj.SetActive(state);

        }
    }
}
