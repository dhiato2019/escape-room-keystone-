using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using System.Collections; // Required for coroutines

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load
    public int requiredQuestValue = 8; // The quest value required to transition
    public FirstPersonController fpc; // Reference to the first person controller

    // This method is called when another collider enters the trigger collider attached to the GameObject
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered is the player
        if (other.CompareTag("Player")) // Ensure the player has the tag "Player"
        {
            // Retrieve the current quest value from PlayerPrefs
            int currentQuestValue = PlayerPrefs.GetInt("quest", 0);

            // Check if the current quest value matches the required value
            if (currentQuestValue == requiredQuestValue)
            {
                Debug.Log("Player collided with NPC, transitioning to scene: " + sceneToLoad);
                fpc.lockCursor = false;

                // Start coroutine to wait for 0.5 seconds before loading the scene
                StartCoroutine(TransitionAfterDelay(0.5f));
            }
            else
            {
                Debug.Log("Quest value does not match. Current quest value: " + currentQuestValue);
            }
        }
    }

    // Coroutine to wait for the delay and then load the new scene
    private IEnumerator TransitionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified time (0.5 seconds)
        SceneManager.LoadScene(sceneToLoad);    // Load the specified scene after the delay
    }
}
