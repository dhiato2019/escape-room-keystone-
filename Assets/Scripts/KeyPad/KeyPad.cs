using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class KeypadUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text displayField; // Assign your TMP_Text for both display and error messages
    [SerializeField] private Button[] numberButtons; // Assign number buttons (0-9) in the inspector
    [SerializeField] private Button clearButton; // Assign the clear button in the inspector
    [SerializeField] private Button confirmButton; // Assign the confirm button in the inspector
    [SerializeField] private Animator targetAnimator; // Assign the Animator component in the inspector
    public FirstPersonController fpc;           // Reference to FirstPersonController (assign in Inspector)

    private bool isError = false; // Flag to indicate if the display is showing an error
    [SerializeField] private float errorDisplayTime = 2f; // Time to display the error before deactivating

    private void Start()
    {
        // Add listeners to buttons
        foreach (var button in numberButtons)
        {
            button.onClick.AddListener(() => OnNumberButtonClick(button));
        }

        clearButton.onClick.AddListener(OnClearButtonClick);
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
    }

    private void OnNumberButtonClick(Button button)
    {
        // Get the number from the button's text and append to the display field
        string number = button.GetComponentInChildren<Text>().text;
        if (isError)
        {
            // If there's an error, clear it before appending new text
            ClearDisplayField();
            isError = false;
        }
        AppendToDisplayField(number);
    }

    private void OnClearButtonClick()
    {
        ClearDisplayField();
    }

    private void OnConfirmButtonClick()
    {
        ConfirmInput();
    }

    private void AppendToDisplayField(string text)
    {
        // Append the number to the display field
        displayField.text += text;
    }

    private void ClearDisplayField()
    {
        // Clear the display field
        displayField.text = "";
    }

    private void ConfirmInput()
    {
        // Validate the input
        string input = displayField.text;

        // Example validation - checking if the input is exactly "156"
      if (input == "156")
{
    Debug.Log("Input confirmed: " + input);
    
    // Set the Animator's "opened" float variable to 1
    targetAnimator.SetFloat("opened", 1f);
    Debug.Log("Setting 'opened' to 1f.");

    // Update the PlayerPrefs variable to quest 2


    // Deactivate the keypad UI object
    gameObject.SetActive(false);
    fpc.lockCursor = true;  // Set to true when unpaused

}

        else
        {
            StartCoroutine(HandleErrorAndDeactivate("Invalid input. Please try again."));
        }
    }

    private IEnumerator HandleErrorAndDeactivate(string message)
    {
        ShowError(message); // Display the error message
        yield return new WaitForSeconds(errorDisplayTime); // Wait for the specified time
        gameObject.SetActive(false); // Deactivate the keypad UI object
         fpc.lockCursor = true;  // Set to true when unpaused

    }

    private void ShowError(string message)
    {
        // Display the error message
        displayField.text = message;
        isError = true; // Set the error flag to true
    }
}
