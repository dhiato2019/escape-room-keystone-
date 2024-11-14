using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;

    private Dictionary<string, string> localizedText;
    private string currentLanguage; // Language will be set from PlayerPrefs

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep the localization manager persistent across scenes
            //PlayerPrefs.SetString("SelectedLanguage","fr");
           // PlayerPrefs.Save();
            // Load saved language from PlayerPrefs, or default to "fr"
            currentLanguage = PlayerPrefs.GetString("SelectedLanguage");
            LoadLocalizedText(currentLanguage);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadLocalizedText(string language)
    {
        currentLanguage = language;
        localizedText = new Dictionary<string, string>();

        TextAsset localizationData = Resources.Load<TextAsset>("localization"); // Load JSON file from Resources
        if (localizationData == null)
        {
            Debug.LogError("Localization file not found!");
            return;
        }

        // Parse JSON data
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(localizationData.text);
        foreach (var item in loadedData.items)
        {
            if (item.language == language)
            {
                localizedText[item.key] = item.value;
            }
        }
    }

    public string GetLocalizedValue(string key)
    {
        if (localizedText.ContainsKey(key))
        {
            return localizedText[key];
        }
        else
        {
            Debug.LogWarning("Localization key not found: " + key);
            return key; // Return the key itself if not found, for easier debugging.
        }
    }

    public void ChangeLanguage(string newLanguage)
    {
        LoadLocalizedText(newLanguage);

        // Save the selected language in PlayerPrefs
        PlayerPrefs.SetString("SelectedLanguage", newLanguage);
        PlayerPrefs.Save();

        // Notify all localized UI elements to update their texts
        foreach (LocalizationText localizedTextComponent in FindObjectsOfType<LocalizationText>())
        {
            localizedTextComponent.UpdateText(); // Update text for all LocalizationText components
        }
    }
}

[System.Serializable]
public class LocalizationData
{
    public List<LocalizationItem> items; // List of localization items
}

[System.Serializable]
public class LocalizationItem
{
    public string language; // Language code, e.g., "en", "fr"
    public string key;      // Key for the text
    public string value;    // Localized value for the key
}
