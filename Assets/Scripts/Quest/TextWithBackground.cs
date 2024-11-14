using UnityEngine;
using TMPro;

public class TextWithBackground : MonoBehaviour
{
    public TextMeshProUGUI tmpText;  // Assign the TMP text object
    public RectTransform background; // Assign the background object

    // Padding around the text (adjust as needed)
    public Vector2 padding = new Vector2(10f, 5f);

    void Start()
    {
        if (tmpText == null)
        {
            tmpText = GetComponent<TextMeshProUGUI>();
        }

        UpdateBackgroundSize();
    }

    void Update()
    {
        UpdateBackgroundSize();
    }

    void UpdateBackgroundSize()
    {
        // Get the preferred width and height of the text
        Vector2 textSize = new Vector2(tmpText.preferredWidth, tmpText.preferredHeight);

        // Adjust the background size to match the text, adding padding
        background.sizeDelta = textSize + padding;
    }
}
