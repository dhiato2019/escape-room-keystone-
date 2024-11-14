using UnityEngine;
using TMPro; // TextMesh Pro namespace

[RequireComponent(typeof(TextMeshPro))] // Ensure TextMeshPro is required
public class LocalizationText : MonoBehaviour
{
    private TextMeshPro textComponent; // Reference to TextMeshPro component

    public string localizationKey; // Key for this text

    private void Start()
    {
        // Try to get the TextMeshPro component
        textComponent = GetComponent<TextMeshPro>();

        // Check if the TextMeshPro component is present
        if (textComponent == null)
        {
            Debug.LogError("TextMeshPro component not found on this GameObject. Please attach a TextMeshPro component.");
            return;
        }

        // Update text immediately on initialization
        UpdateText();
    }

    public void UpdateText()
    {
        if (textComponent != null && !string.IsNullOrEmpty(localizationKey))
        {
            // Retrieve the localized text from the LocalizationManager
            string localizedText = LocalizationManager.Instance.GetLocalizedValue(localizationKey);
            textComponent.text = localizedText;
        }
    }
}
