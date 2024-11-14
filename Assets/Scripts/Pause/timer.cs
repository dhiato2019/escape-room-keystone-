using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // If you are using TextMeshPro for text display
using UnityEngine.UI; // Make sure to include this for Image
using UnityEngine.SceneManagement; // Add this for scene management

public class Timer : MonoBehaviour
{
    public float timeRemaining = 1200f; // 20 minutes in seconds (20 * 60)
    [SerializeField] private TMP_Text timerText; // Reference to the UI Text component
    [SerializeField] private Image fillImage; // Reference to the circular fill image
public FirstPersonController fpc;
    public bool timerIsRunning = false; // To check if the timer is running
    private float totalTime; // Total time in seconds

    void Start()
    {
        totalTime = timeRemaining; // Store the total time
        StartTimer(); // Start the timer when the game starts
        UpdateTimerDisplay(); // Initialize the display
        UpdateFillAmount(); // Initialize the fill amount
    }

    void Update()
    {
        if (timerIsRunning)
        { 
            int number = PlayerPrefs.GetInt("quest");
            if (timeRemaining > 0)
            {
                // Decrease the time remaining
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(); // Update the display each frame
                UpdateFillAmount(); // Update the fill amount each frame

                // Check if the time remaining is less than 20% of the total time
                if (timeRemaining < totalTime * 0.2f)
                {
                    SetTimerWarningColors(); // Change colors to red if below 20%
                }
            }
            else
            {
                // Time has run out
                timeRemaining = 0;
                timerIsRunning = false;
                UpdateTimerDisplay(); // Ensure the display shows 0:00
                UpdateFillAmount(); // Ensure the fill amount is set to 0
                OnTimerEnd(); // Trigger any event on timer end
            }
        }
    }

    // Updates the timer display format in MM:SS
    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60); // Calculate minutes
        int seconds = Mathf.FloorToInt(timeRemaining % 60); // Calculate seconds

        // Update the text display to show the time in MM:SS format
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Updates the fill amount based on remaining time
    private void UpdateFillAmount()
    {
        fillImage.fillAmount = timeRemaining / totalTime; // Normalize the fill amount based on total time (20 minutes)
    }

    // Change colors of the timer and fill image to red
    private void SetTimerWarningColors()
    {
        fillImage.color = Color.red; // Change fill image color to red
        timerText.color = Color.red; // Change timer text color to red
    }

    // This method will be called when the timer ends
    private void OnTimerEnd()
    {
        fpc.lockCursor = false; // Ensure cursor is unlocked
        StartCoroutine(LoadWastedScene()); // Start the coroutine to load the scene
    }

    // Coroutine to wait before loading the "Wasted" scene
    private IEnumerator LoadWastedScene()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        SceneManager.LoadScene("Wasted"); // Load the "Wasted" scene
    }

    // Method to start the timer
    public void StartTimer()
    {
        timerIsRunning = true;
    }

    // Method to stop the timer
    public void StopTimer()
    {
        timerIsRunning = false;
    }
}
