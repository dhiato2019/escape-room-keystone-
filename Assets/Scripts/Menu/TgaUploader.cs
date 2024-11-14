using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SFB; // Namespace for Standalone File Browser

public class PngUploader : MonoBehaviour
{
    public Button uploadButton;
    public GameObject prefab; // The prefab containing the material

    private const string TexturePathKey = "LastUploadedTexturePath";

    private void Start()
    {
        uploadButton.onClick.AddListener(OpenFileDialog);

        // Check if a texture path is stored in PlayerPrefs and load it
        if (PlayerPrefs.HasKey(TexturePathKey))
        {
            string savedPath = PlayerPrefs.GetString(TexturePathKey);
            if (File.Exists(savedPath)) // Check if the file still exists
            {
                StartCoroutine(LoadTexture(savedPath));
            }
            else
            {
                Debug.LogWarning("Saved texture path does not exist.");
            }
        }
    }

    private void OpenFileDialog()
    {
        var extensions = new[] {
            new ExtensionFilter("Image Files", "png"),
            new ExtensionFilter("All Files", "*" ),
        };
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Select PNG file", "", extensions, false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            StartCoroutine(LoadTexture(paths[0]));
            PlayerPrefs.SetString(TexturePathKey, paths[0]); // Save the path to PlayerPrefs
            PlayerPrefs.Save(); // Ensure it is saved
        }
        else
        {
            Debug.LogError("No file selected.");
        }
    }

    private IEnumerator LoadTexture(string path)
    {
        string filePath = "file://" + path;

        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(filePath))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + uwr.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                if (texture != null)
                {
                    ApplyTexture(texture);
                }
                else
                {
                    Debug.LogError("Failed to load texture.");
                }
            }
        }
    }

    private void ApplyTexture(Texture2D texture)
    {
        if (prefab != null)
        {
            Renderer renderer = prefab.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material[] materials = renderer.sharedMaterials;
                bool materialFound = false;

                foreach (Material material in materials)
                {
                    if (material.name == "MAT_bank_posters_01")
                    {
                        Debug.Log("Applying texture to material: " + material.name);
                        material.SetTexture("_MainTex", texture); // Adjust if necessary
                        materialFound = true;
                        break;
                    }
                }

                if (!materialFound)
                {
                    Debug.LogError("Material 'MAT_bank_posteres_01' not found.");
                }
            }
            else
            {
                Debug.LogError("No Renderer found on prefab.");
            }
        }
        else
        {
            Debug.LogError("Prefab not assigned.");
        }
    }
}
