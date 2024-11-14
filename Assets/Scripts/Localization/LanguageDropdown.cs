using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro

public class LanguageDropdown : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown languageDropdown; // Reference to the TMP_Dropdown component
    [SerializeField] private Sprite frenchFlag; // Reference to the French flag sprite
    [SerializeField] private Sprite britishFlag; // Reference to the British flag sprite

    private void Start()
    {
        // Populate dropdown options
        languageDropdown.ClearOptions();
        var options = new TMP_Dropdown.OptionDataList
        {
            options = new List<TMP_Dropdown.OptionData>
            {
                new TMP_Dropdown.OptionData("English"),
                new TMP_Dropdown.OptionData("French")
            }
        };
        languageDropdown.AddOptions(options.options);
       // PlayerPrefs.SetString("SelectedLanguage", "fr");
       // PlayerPrefs.Save();
        // Load previously saved language or default to English
        string savedLanguage = PlayerPrefs.GetString("SelectedLanguage");
        int savedIndex = options.options.FindIndex(option => option.text.Equals("English", System.StringComparison.OrdinalIgnoreCase));
        
        // If the saved language is invalid or not found, default to English
        if (savedLanguage != "en" && savedLanguage != "fr")
        {
            savedLanguage = "fr"; // Default to English if saved language is invalid
            PlayerPrefs.SetString("SelectedLanguage", savedLanguage);
            PlayerPrefs.Save();
        }
        savedIndex = options.options.FindIndex(option => option.text.Equals(savedLanguage == "en" ? "English" : "French", System.StringComparison.OrdinalIgnoreCase));
        
        languageDropdown.value = savedIndex;

        // Set the initial icon based on the saved language
        UpdateDropdownIcon(savedLanguage);

        // Add listener for dropdown value changes
        languageDropdown.onValueChanged.AddListener(OnLanguageSelected);
    }

    private void OnLanguageSelected(int index)
    {
        // Get the selected language
        string selectedLanguage = languageDropdown.options[index].text;

        // Map language name to code (e.g., "English" to "en")
        string languageCode = selectedLanguage == "French" ? "fr" : "en";

        // Save the selected language
        PlayerPrefs.SetString("SelectedLanguage", languageCode);
        PlayerPrefs.Save();

        // Update language in the game
        LocalizationManager.Instance.ChangeLanguage(languageCode);

        // Update the dropdown icon based on the selected language
        UpdateDropdownIcon(languageCode);
    }

    private void UpdateDropdownIcon(string languageCode)
    {
        // Update the icon based on the language code
        switch (languageCode)
        {
            case "fr":
                languageDropdown.image.sprite = frenchFlag;
                break;
            case "en":
                languageDropdown.image.sprite = britishFlag;
                break;
        }
    }
}
