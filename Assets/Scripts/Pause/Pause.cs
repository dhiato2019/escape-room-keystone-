using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Button yourButton;                   // Assign the button through the inspector
    public GameObject objectToToggle;           // Object to toggle (e.g., pause menu)
    public GameObject crosshairAndStamina;      // Assign the Crosshair and Stamina GameObject in the inspector
    public Timer timer;                         // Assign the Timer script or GameObject with Timer in the inspector
    public FirstPersonController fpc;           // Reference to FirstPersonController (assign in Inspector)

    private bool isPaused = true;               // Tracks whether the game is paused

    void Start()
    {
        // Ensure paused state is set at the beginning
        PlayerPrefs.SetInt("paused", 1);

        // Ensure that the button is assigned
        if (yourButton == null)
        {
            Debug.LogError("Button is not assigned.");
            return;
        }

        // Set up the button click listener to toggle the pause state
        yourButton.onClick.AddListener(OnButtonClick);

        // Ensure the Crosshair and Stamina object is assigned
        if (crosshairAndStamina == null)
        {
            Debug.LogError("Crosshair and Stamina GameObject is not assigned.");
        }

        // Ensure the Timer script is assigned
        if (timer == null)
        {
            Debug.LogError("Timer script is not assigned.");
        }

        // Ensure the FirstPersonController is assigned
        if (fpc == null)
        {
            Debug.LogError("FirstPersonController is not assigned.");
        }
    }

    // Called when the button is clicked
    void OnButtonClick()
    {
        TogglePauseState();
    }

    // Toggles the pause state
    void TogglePauseState()
    {
        // Toggle the paused state
        isPaused = !isPaused;

        // Update the PlayerPrefs based on the pause state
        PlayerPrefs.SetInt("paused", isPaused ? 1 : 0);

        // Activate/Deactivate the pause menu (or any other UI element assigned to objectToToggle)
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(isPaused);
        }

        // Activate/Deactivate the crosshair and stamina UI
        if (crosshairAndStamina != null)
        {
            crosshairAndStamina.SetActive(!isPaused);
        }

        // Start or stop the timer based on the pause state
        if (timer != null)
        {
            if (isPaused)
            {
                timer.StopTimer();
            }
            else
            {
                timer.StartTimer();
            }
        }

        // Set lockCursor to true when the game is unpaused
        if (fpc != null)
        {
            fpc.lockCursor = !isPaused;  // Set to true when unpaused
        }
    }
}
