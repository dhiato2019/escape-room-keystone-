using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject objectToToggle;  // The GameObject to activate/deactivate (e.g., your pause menu)
    public GameObject cross;           // Reference to the GameObject (e.g., crosshair or HUD)
    public Timer timer;                // Reference to the Timer script
    public AudioSource audioSource;    // Reference to the AudioSource to pause/resume audio
    public FirstPersonController fpc;  // Reference to the FirstPersonController script

    private bool isPaused = false;     // To track whether the game is paused

    private void Start()
    {
        // Ensure the pause menu starts inactive
        if (objectToToggle != null)
        {
            objectToToggle.SetActive(false);
        }

        // Optionally, make sure cross is active when the game starts
        if (cross != null)
        {
            cross.SetActive(true);
        }

        // Ensure the audio source is playing at the start
        if (audioSource != null)
        {
            audioSource.Play();
        }
        
        // Optionally, lock the cursor at the start
        if (fpc != null)
        {
            fpc.lockCursor = true;
        }
    }

    private void Update()
    {
        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle pause state
            isPaused = !isPaused;

            // If paused, activate the pause menu and stop the timer
            if (isPaused)
            {
                // Activate the pause menu
                if (objectToToggle != null)
                {
                    objectToToggle.SetActive(true);
                }

                // Deactivate the crosshair or any other UI element
                if (cross != null)
                {
                    cross.SetActive(false);
                }

                // Stop the timer if the Timer script is found
                if (timer != null)
                {
                    timer.StopTimer();
                }

                // Pause the audio if the AudioSource is assigned
                if (audioSource != null)
                {
                    audioSource.Pause();
                }

                // Disable the cursor lock when the game is paused
                if (fpc != null)
                {
                    fpc.lockCursor = false;
                }
            }
            else
            {
                // Unpause: deactivate the pause menu and restart the timer
                if (objectToToggle != null)
                {
                    objectToToggle.SetActive(false);
                }

                // Activate the crosshair again
                if (cross != null)
                {
                    cross.SetActive(true);
                }

                // Restart the timer if the Timer script is found
                if (timer != null)
                {
                    timer.StartTimer();
                }

                // Resume the audio if the AudioSource is assigned
                if (audioSource != null)
                {
                    audioSource.UnPause();
                }

                // Enable the cursor lock when the game is unpaused
                if (fpc != null)
                {
                    fpc.lockCursor = true;
                }
            }
        }
    }
}
