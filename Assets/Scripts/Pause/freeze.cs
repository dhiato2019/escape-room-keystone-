using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject prefabToTrack;                   // The prefab to track
    public MonoBehaviour firstPlayerControllerScript; // Reference to the First Player Controller script
    public Timer timerScript;                         // Reference to the Timer script
public GameObject additionalGameObject ;
    private GameObject trackedPrefabInstance;

    private void Start()
    {
        if (prefabToTrack != null && timerScript != null && firstPlayerControllerScript != null)
        {
            StartCoroutine(CheckForPrefab());
        }
        else
        {
            Debug.LogError("Prefab to track, Timer script, or First Player Controller script is not assigned.");
        }
    }

   private IEnumerator CheckForPrefab()
{
    while (true)
    {
        // Check if the prefab is in the scene
        GameObject currentPrefabInstance = GameObject.Find(prefabToTrack.name);

        if (currentPrefabInstance != null)
        {
            // If the prefab is found and the previous instance was not found
            if (trackedPrefabInstance == null)
            {
                Debug.Log("Prefab found.");

                // Stop the timer and disable the First Player Controller script
                if (timerScript != null)
                {
                    timerScript.StopTimer(); // Stop the timer
                    Debug.Log("Timer stopped.");
                }

                if (firstPlayerControllerScript != null)
                {
                    firstPlayerControllerScript.enabled = false; // Disable the script
                    Debug.Log("First Player Controller script disabled.");
                }

                if (additionalGameObject != null)
                {
                    additionalGameObject.SetActive(false); // Disable the additional GameObject
                    Debug.Log("Additional GameObject disabled.");
                }

                trackedPrefabInstance = currentPrefabInstance; // Update the tracked instance
            }
        }
        else
        {
            // If the prefab is not found and it was previously tracked
            
                Debug.Log("Prefab destroyed.");

                // Check the PlayerPrefs variable
                if (PlayerPrefs.GetInt("paused", 0) == 0)
                {
                    // Re-enable the First Player Controller script, resume the timer, and set the additional GameObject active
                    if (timerScript != null)
                    {
                        timerScript.StartTimer(); // Resume the timer
                        Debug.Log("Timer started.");
                    }

                    if (firstPlayerControllerScript != null)
                    {
                        firstPlayerControllerScript.enabled = true; // Re-enable the script
                        Debug.Log("First Player Controller script enabled.");
                    }

                    if (additionalGameObject != null)
                    {
                        additionalGameObject.SetActive(true); // Enable the additional GameObject
                        Debug.Log("Additional GameObject enabled.");
                    }
                }

            }
        

        yield return new WaitForSeconds(0.5f); // Check every 0.5 seconds
    }
}

}
