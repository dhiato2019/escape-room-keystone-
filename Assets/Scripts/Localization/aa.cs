using UnityEngine;
using TMPro;

public class aa : MonoBehaviour
{
    [SerializeField] private string key; // Key for the localized text
    private TextMeshProUGUI textMeshPro; // Reference to the TMP component

    private void Awake()
    {
        // Get the TextMeshPro component attached to this GameObject
        textMeshPro = GetComponent<TextMeshProUGUI>();
        
        // Update the text initially when the object is created
        UpdateText();
    }

    // Method to update the text based on the current language
    public void UpdateText()
    {
        if (textMeshPro != null)
        {
            // Get the localized value from LocalizationManager using the provided key
            string localizedValue = LocalizationManager.Instance.GetLocalizedValue(key);
            textMeshPro.text = localizedValue; // Set the localized text
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found!");
        }
    }
}
