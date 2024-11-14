using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private const string VolumePrefKey = "GameVolume"; // Key to store volume in PlayerPrefs

    private void Start()
    {
        // Load the saved volume from PlayerPrefs, or set to 1 (max volume) if not set
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 1f);
        AudioListener.volume = savedVolume;

        // Set the slider value to the saved volume
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChange);
        }
    }

    private void OnVolumeChange(float value)
    {
        // Change the global audio volume
        AudioListener.volume = value;

        // Save the volume setting
        PlayerPrefs.SetFloat(VolumePrefKey, value);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        // Remove the listener when the object is destroyed
        if (volumeSlider != null)
        {
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChange);
        }
    }
}
