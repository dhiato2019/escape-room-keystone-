using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class TimerObjectActivator : MonoBehaviour
{
    [SerializeField] private Timer timer; // Reference to the Timer script
    [SerializeField] private string sceneName = "wasted"; // Name of the scene to load

    void Update()
    {
        // Check if the timer has reached 0
        if (!timer.timerIsRunning && timer.timeRemaining == 0)
        {
            LoadWastedScene(); // Transition to another scene
        }
    }

    // Method to activate the object when the timer ends


    // Method to load the 'wasted' scene
    private void LoadWastedScene()
    {
        SceneManager.LoadScene(sceneName); // Load the scene with the specified name
    }
}
