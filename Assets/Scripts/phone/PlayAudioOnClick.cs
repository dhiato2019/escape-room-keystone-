using UnityEngine;
using UnityEngine.UI;

public class PlayAudiophone : MonoBehaviour
{
    public Button assignedButton; // Button to be assigned in the Unity Editor
    public AudioSource audioSource; // AudioSource to be assigned in the Unity Editor
    public AudioClip englishAudioClip; // English AudioClip
    public AudioClip frenchAudioClip;  // French AudioClip

    void Start()
    {
        // Check if the assignedButton and audioSource are set
        if (assignedButton != null && audioSource != null)
        {
            // Add a listener to the button to call the PlayAudio function when clicked
            assignedButton.onClick.AddListener(PlayAudio);
        }
        else
        {
            Debug.LogWarning("Button or AudioSource is not assigned in the inspector.");
        }
    }

    void PlayAudio()
    {
        if (audioSource != null)
        {
            // Determine which audio clip to play based on the selectedLanguage PlayerPref
            string selectedLanguage = PlayerPrefs.GetString("SelectedLanguage");

            if (selectedLanguage == "en" && englishAudioClip != null)
            {
                audioSource.clip = englishAudioClip;
            }
            else if (selectedLanguage == "fr" && frenchAudioClip != null)
            {
                audioSource.clip = frenchAudioClip;
            }
            else
            {
                Debug.LogWarning("AudioClip for the selected language is not assigned.");
                return;
            }

            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned in the inspector.");
        }
    }
}
