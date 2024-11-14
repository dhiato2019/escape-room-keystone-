using UnityEngine;
using System.Collections.Generic; // For handling lists

public class PauseChecker : MonoBehaviour
{
    // List of game objects to disable when "Pause (Clone)" is detected
    public List<GameObject> objectsToDisable;

    // Reference to the "Pause (Clone)" object
    public GameObject pauseClone;

    void Update()
    {
        
    }

    // Optional: Call this method when "Pause (Clone)" is spawned in the game

}
