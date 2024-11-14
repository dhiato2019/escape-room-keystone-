using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Use this if you're using TextMeshPro

public class PaperViewer : MonoBehaviour
{
    public Image spriteRenderer;  // Image component to display the sprite
    public Button leftButton;     // Button to navigate left
    public Button rightButton;    // Button to navigate right
    public TextMeshProUGUI indexText;  // Text component to display the current index (use Text if you're using Legacy Text)

    public Sprite[] sprites;  // Array of sprites to assign
    private int currentIndex = 0;  // Current index of the sprite being displayed

    void Start()
    {
        if (sprites.Length == 0)
        {
            Debug.LogWarning("No sprites assigned!");
            return;
        }

        // Set initial sprite and index
        UpdateSprite();

        // Add listeners to the buttons
        leftButton.onClick.AddListener(ShowPreviousSprite);
        rightButton.onClick.AddListener(ShowNextSprite);
    }

    void ShowPreviousSprite()
    {
        if (sprites.Length == 0) return;

        currentIndex--;
        if (currentIndex < 0) currentIndex = sprites.Length - 1;  // Loop back to the last sprite
        UpdateSprite();
    }

    void ShowNextSprite()
    {
        if (sprites.Length == 0) return;

        currentIndex++;
        if (currentIndex >= sprites.Length) currentIndex = 0;  // Loop back to the first sprite
        UpdateSprite();
    }

    void UpdateSprite()
    {
        spriteRenderer.sprite = sprites[currentIndex];  // Update the displayed sprite
        indexText.text = $"{currentIndex + 1} / {sprites.Length}";  // Display index out of total
    }
}
