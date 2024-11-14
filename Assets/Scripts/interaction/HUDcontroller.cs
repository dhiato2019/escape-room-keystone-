using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;

    [SerializeField] private TMP_Text interactionText; // Use TMP_Text component
    [SerializeField] private Camera playerCamera; // Assign your player camera in the inspector
    [SerializeField] private float rayDistance = 2f; // Distance for the raycast

    // This variable will help us determine if a controller is connected
    private bool isControllerConnected;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // Check for controller connection at the start
        isControllerConnected = IsControllerConnected();
    }

    private void FixedUpdate()
    {
        RaycastForInteractable();
    }

    private void RaycastForInteractable()
    {
        // Continuously cast a ray from the camera forward
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        // Check if the ray hits an object within the specified distance
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            // Check if the object has the "canPickUp" tag
            if (hit.collider.CompareTag("canPickUp") || hit.collider.CompareTag("badge"))
            {
                ShowLocalizedInteractionText(isControllerConnected ? "in_game_controller_pick_up" : "in_game_pickup");
            }
            // Check if the object has the "Interactable" tag
            else if (hit.collider.CompareTag("Interactable"))
            {
                ShowLocalizedInteractionText(isControllerConnected ? "in_game_controller_interact" : "in_game_interact");
            }
            // Check if the object has the "door" tag
            else if (hit.collider.CompareTag("door"))
            {
                ShowLocalizedInteractionText("door_interact"); // This can remain the same for now
            }
            else
            {
                // Hide the interaction text if the tag doesn't match
                HideInteractionText();
            }
        }
        else
        {
            // Hide the interaction text if nothing is hit
            HideInteractionText();
        }
    }

    public void ShowLocalizedInteractionText(string localizationKey)
    {
        // Retrieve the localized text from the LocalizationManager
        string localizedText = LocalizationManager.Instance.GetLocalizedValue(localizationKey);
        
        // Only enable the text if it isn't already visible
        if (!interactionText.gameObject.activeSelf || interactionText.text != localizedText)
        {
            interactionText.text = localizedText;
            interactionText.gameObject.SetActive(true);
            Debug.Log("Displaying Interaction Text: " + localizedText);
        }
    }

    public void HideInteractionText()
    {
        // Only disable the text if it is currently visible
        if (interactionText.gameObject.activeSelf)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    // Method to check if a controller is connected
    private bool IsControllerConnected()
    {
        // You can use Unity's Input System or legacy Input here. 
        // This example assumes you're using the legacy Input System.
        return Input.GetJoystickNames().Length > 0 && !string.IsNullOrEmpty(Input.GetJoystickNames()[0]);
    }
}
