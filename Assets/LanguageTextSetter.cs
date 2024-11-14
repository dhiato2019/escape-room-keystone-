using UnityEngine;
using TMPro;

public class LanguageTextSetter : MonoBehaviour
{
    // Reference to the TextMeshPro component
    public TextMeshProUGUI textComponent;

    // Text options for each language
    public string textEN = "Insert the krypt key into the server and press E.";
    public string textFR = "Mettez la clé krypt dans le serveur et appuyez sur E.";

    void Start()
    {
        // Get the TextMeshPro component attached to the GameObject

        if (textComponent == null)
        {
            Debug.LogError("TextMeshPro component not found. Attach this script to a GameObject with a TextMeshProUGUI component.");
            return;
        }

        // Set the initial text based on the selected language
        SetTextBasedOnLanguage();
    }

    void SetTextBasedOnLanguage()
    {
        // Get the selected language from PlayerPrefs (default to "en" if not set)
        string selectedLanguage = PlayerPrefs.GetString("SelectedLanguage", "en");

        // Set text based on language
        if (selectedLanguage == "en")
        {
            textComponent.text = textEN;
        }
        else if (selectedLanguage == "fr")
        {
            textComponent.text = textFR;
        }
        else
        {
            Debug.LogWarning("Language not recognized. Defaulting to English.");
            textComponent.text = textEN;
        }
    }
}
