using UnityEngine;
using UnityEngine.UI;

public class LockToDefaultInputFieldBackground : MonoBehaviour
{
    [SerializeField] private Image targetImage; // The Image component to monitor

    private Sprite defaultInputFieldBackground; // Reference to the default sprite

    void Start()
    {
        if (targetImage == null)
        {
            // Try to get the Image component on this GameObject if not assigned in Inspector
            targetImage = GetComponent<Image>();
        }

        if (targetImage != null)
        {
            // Set the defaultInputFieldBackground sprite
            defaultInputFieldBackground = Resources.Load<Sprite>("UI/Skin/InputFieldBackground"); // Path to default sprite

            if (defaultInputFieldBackground != null)
            {
                // Lock the image to the default sprite
                targetImage.sprite = defaultInputFieldBackground;
            }
            else
            {
                Debug.LogError("Default InputFieldBackground sprite not found!");
            }
        }
        else
        {
            Debug.LogError("No Image component found or assigned!");
        }
    }

    void Update()
    {
        if (targetImage != null && targetImage.sprite != defaultInputFieldBackground)
        {
            // Revert the sprite to the default sprite if it has been changed
            Debug.LogWarning($"Sprite changed to: {targetImage.sprite.name}. Reverting to default InputFieldBackground.");
            targetImage.sprite = defaultInputFieldBackground;
        }
    }
}
