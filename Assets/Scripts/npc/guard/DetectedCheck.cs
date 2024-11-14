using System.Collections;
using UnityEngine;

public class DetectedCheck : MonoBehaviour
{
    // Reference to the GameObject you want to make appear
    public GameObject targetObject;

    // Timer duration in seconds
    public float displayDuration = 4.0f;

    // Variable to track if the coroutine is already running
    private bool isCoroutineRunning = false;

    // Variable to track if the object has been displayed already
    private bool hasDisplayed = false;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the object starts hidden
        targetObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check PlayerPrefs value
        if (PlayerPrefs.GetInt("detected", 0) == 1 && !isCoroutineRunning && !hasDisplayed)
        {
            // If "detected" is 1 and coroutine isn't already running, start it
            StartCoroutine(DisplayObjectForTime());
        }
    }

    // Coroutine to show the object for 4 seconds
    IEnumerator DisplayObjectForTime()
    {
        isCoroutineRunning = true;

        // Show the object
        targetObject.SetActive(true);

        // Wait for 4 seconds
        yield return new WaitForSeconds(displayDuration);

        // Hide the object
        targetObject.SetActive(false);

        // Mark the object as displayed
        hasDisplayed = true;

        // Reset the coroutine state
        isCoroutineRunning = false;
    }
}
