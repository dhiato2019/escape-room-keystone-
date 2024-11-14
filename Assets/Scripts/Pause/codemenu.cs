using UnityEngine;

public class codemenu : MonoBehaviour
{
    public GameObject prefabToSpawn;  // Drag your prefab here in the Inspector
    private GameObject spawnedPrefab; // Reference to the currently spawned prefab

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))  // Change to KeyCode.Return for the Enter key
        {
            Debug.Log("Enter key pressed.");  // Log when the Enter key is pressed

            if (spawnedPrefab != null)
            {
                Debug.Log("Destroying existing prefab.");  // Log prefab destruction
                Destroy(spawnedPrefab);  // Destroy the previously spawned prefab
                spawnedPrefab = null;    // Clear the reference
            }
            else
            {
                Debug.Log("Spawning new prefab.");  // Log prefab spawning
                SpawnPrefab();  // Spawn a new prefab if none exists
            }
        }
    }

    private void SpawnPrefab()
    {
        if (prefabToSpawn != null)
        {
            // Instantiate the prefab 5 units in front of the camera to ensure visibility
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 5;
            spawnedPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            Debug.Log("Prefab spawned at position: " + spawnPosition);  // Log the spawn position
        }
        else
        {
            Debug.LogWarning("Prefab to spawn is not assigned.");  // Warn if the prefab is not assigned
        }
    }
}
