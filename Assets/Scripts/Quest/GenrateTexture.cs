using UnityEngine;

public class NoteSpriteWithNumber : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int textureWidth = 256;  // Width of the texture
    public int textureHeight = 256; // Height of the texture
    public Color noteColor = Color.white;
    public Color textColor = Color.black;
    public int fontSize = 64;

    private Font font;

    void Start()
    {
        // Load the built-in font
        font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");

        // Create a new Texture2D
        Texture2D texture = new Texture2D(textureWidth, textureHeight);
        Color[] pixels = new Color[textureWidth * textureHeight];
        
        // Fill texture with white background
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = noteColor;
        }
        texture.SetPixels(pixels);

        // Draw a note shape (e.g., a rounded rectangle) on the texture
        DrawNoteShape(texture);

        // Draw the number on the texture
        DrawTextOnTexture(texture, "156", fontSize, textColor);

        // Apply the texture changes
        texture.Apply();

        // Create a new sprite from the texture
        Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, textureWidth, textureHeight), new Vector2(0.5f, 0.5f));
        
        // Assign the new sprite to the SpriteRenderer
        spriteRenderer.sprite = newSprite;
    }

    void DrawNoteShape(Texture2D texture)
    {
        int borderSize = 10;
        Color borderColor = Color.black;

        for (int x = 0; x < textureWidth; x++)
        {
            for (int y = 0; y < textureHeight; y++)
            {
                if (x < borderSize || x >= textureWidth - borderSize || y < borderSize || y >= textureHeight - borderSize)
                {
                    texture.SetPixel(x, y, borderColor);
                }
                else
                {
                    texture.SetPixel(x, y, noteColor);
                }
            }
        }
    }

    void DrawTextOnTexture(Texture2D texture, string text, int fontSize, Color textColor)
    {
        // Create a temporary RenderTexture
        RenderTexture renderTexture = RenderTexture.GetTemporary(textureWidth, textureHeight, 0, RenderTextureFormat.ARGB32);
        RenderTexture.active = renderTexture;

        // Create a temporary GUIStyle for text
        GUIStyle style = new GUIStyle();
        style.font = font;
        style.fontSize = fontSize;
        style.normal.textColor = textColor;
        style.alignment = TextAnchor.MiddleCenter;

        // Create a temporary texture to draw text
        Texture2D textTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false);
        RenderTexture.active = renderTexture;
        textTexture.ReadPixels(new Rect(0, 0, textureWidth, textureHeight), 0, 0);
        textTexture.Apply();

        // Draw text on the temporary texture
        GUI.Label(new Rect(0, 0, textureWidth, textureHeight), text, style);
        
        // Copy the text texture pixels to the main texture
        texture.SetPixels(textTexture.GetPixels());

        // Apply the texture changes
        texture.Apply();

        // Clean up
        RenderTexture.ReleaseTemporary(renderTexture);
        Destroy(textTexture);
    }
}
