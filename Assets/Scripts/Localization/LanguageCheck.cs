using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LanguageCheck : MonoBehaviour
{
    // Reference to the UI Text component where the message will be displayed
    public TextMeshProUGUI messageText; // Reference to the TMP component

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve the selected language from PlayerPrefs
        string selectedLanguage = PlayerPrefs.GetString("SelectedLanguage"); // Default to English if not set

        // Display the appropriate message based on the selected language
        DisplayMessage(selectedLanguage);
    }

    // Method to display the message based on the selected language
    private void DisplayMessage(string language)
    {
        // Check if the language is English
        if (language.Equals("en", System.StringComparison.OrdinalIgnoreCase))
        {
            messageText.text = "The guard must not see you holding sensitive items.";
        }
        // Check if the language is French
        else if (language.Equals("fr", System.StringComparison.OrdinalIgnoreCase))
        {
            messageText.text = "Le garde ne doit pas vous voir tenir des objets sensibles.";
        }
        // If the language is not recognized, display a default message
        else
        {
            messageText.text = "Language not recognized. Please set to English or French.";
        }
    }
}
