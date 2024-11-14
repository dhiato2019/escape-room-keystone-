using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MFAcode : MonoBehaviour
{
    public TMP_InputField inputField; // Reference to the TMP_InputField component
    public GameObject objectToActivate; // The GameObject to activate
    public GameObject additionalobjectToActivate; // The GameObject to activate
    public GameObject objectToActivate1; // The GameObject to activate
    public GameObject objectToDestroy; // The GameObject to destroy (assignable)
    public GameObject additionalObjectToDestroy; // New: Additional object to destroy after 1 second
    public string correctCode ; // The correct code

    private void Start()
    {
        Button button = GetComponent<Button>();

        // Ensure a Button component is attached
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("Button component not found on this GameObject.");
        }
    }

    private void OnButtonClick()
    {
        // Check if the TMP_InputField is assigned
        if (inputField != null)
        {
            string enteredCode = inputField.text.Trim(); // Trim whitespace from the entered code

            // Check if the entered code matches the correct code
            if (enteredCode == correctCode)
            {
                    PlayerPrefs.SetInt("quest", 7);
                    PlayerPrefs.Save(); 
                
                if (objectToActivate != null)
                {
                    objectToActivate.SetActive(true);
// Make sure to save the changes to PlayerPrefs
                }
                else
                {
                    Debug.LogWarning("Object to activate is not assigned.");
                }
                if (objectToActivate1 != null)
                {
                    objectToActivate1.SetActive(true);
                }
                else
                {
                    Debug.LogWarning("Object to activate is not assigned.");
                }
                if (additionalobjectToActivate != null)
                {
                    additionalobjectToActivate.SetActive(true);
                }
                else
                {
                    Debug.LogWarning("Object to activate is not assigned.");
                }
                // Destroy the assigned object, if it exists
                if (objectToDestroy != null)
                {
                    Destroy(objectToDestroy);
                }
                else
                {
                    Debug.LogWarning("Object to destroy is not assigned.");
                }

                // New: Destroy the additional object after 1 second
                if (additionalObjectToDestroy != null)
                {
                    Destroy(additionalObjectToDestroy, 0.1f);
                }
                else
                {
                    Debug.LogWarning("Additional object to destroy is not assigned.");
                }
            }
            else
            {
                // Code is incorrect, display error message
                inputField.text = "ERREUR ? LE MOT DE PASSE EST ERRON2";
            }
        }
        else
        {
            Debug.LogError("TMP_InputField component is not assigned.");
        }
    }
}
