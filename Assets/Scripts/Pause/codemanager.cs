using UnityEngine;
using UnityEngine.UI;

public class codemanager : MonoBehaviour
{
    public InputField[] inputFields; // Array to hold the InputFields

    private void Start()
    {
        // Loop through each InputField to initialize and load saved data
        for (int i = 0; i < inputFields.Length; i++)
        {
            int index = i;  // Capture the current index for the delegate

            // Generate a unique key for each InputField
            string key = "InputField" + index;

            // Check if the key exists in PlayerPrefs and load the saved text
            if (PlayerPrefs.HasKey(key))
            {
                inputFields[index].text = PlayerPrefs.GetString(key);  // Load the saved text
            }

            // Add listener to save the text whenever it changes in the InputField
            inputFields[index].onValueChanged.AddListener((value) => SaveInputFieldText(index, value));
        }
    }

    // Method to save the input text to PlayerPrefs whenever it changes
    private void SaveInputFieldText(int index, string text)
    {
        string key = "InputField" + index;  // Generate a unique key for each InputField
        PlayerPrefs.SetString(key, text);   // Save the text to PlayerPrefs
        PlayerPrefs.Save();                 // Ensure the changes are saved immediately
        Debug.Log($"Saved {text} to {key}"); // Optional: Debug log to verify saving
    }
}
