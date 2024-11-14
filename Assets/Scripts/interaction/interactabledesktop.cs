using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactabledesktop : MonoBehaviour
{
    [SerializeField] private Camera playerCamera; // Assign your player camera in the inspector
    [SerializeField] private float rayDistance = 5f; // Distance for the raycast
    [SerializeField] private float interactionDistance = 5f; // Distance for interaction
    [SerializeField] private KeyCode interactionKey = KeyCode.F; // Key to press for interaction
    [SerializeField] private GameObject targetObject; // Assign the GameObject to toggle active state in the inspector

    private void Update()
    {
        // Continuously cast a ray from the camera forward
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Check if the ray hits an object within the specified distance
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log("Raycast hit: " + hit.collider.name); // Debug log for what the raycast hit

            // Check if the object has the "Interactable" tag
            if (hit.collider.CompareTag("Interactable"))
            {
                // Check if the player presses the interaction key and the object is within distance
                if (Input.GetKeyDown(interactionKey) /*|| Input.GetButtonDown("ControllerA") */&& Vector3.Distance(playerCamera.transform.position, hit.point) <= interactionDistance)
                {
                    Debug.Log($"Interaction key pressed on: {gameObject.name} and within distance."); // Debug log for interaction

                    // Toggle active state of the assigned target object
                    ToggleActiveState(targetObject);
                }
            }
        }
    }

    private void ToggleActiveState(GameObject obj)
    {
        if (obj != null)
        {
            // Toggle the active state of the GameObject
            obj.SetActive(!obj.activeSelf);
            Debug.Log($"{gameObject.name}: Target object active state toggled: {obj.activeSelf}"); // Debug log for active state toggle
        }
        else
        {
            Debug.LogError("Target object is not assigned.");
        }
    }
}
