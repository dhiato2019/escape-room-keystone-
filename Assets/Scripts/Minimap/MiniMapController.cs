using UnityEngine;

public class MiniMapController : MonoBehaviour
{
    public GameObject player;  // The player GameObject
    public GameObject prefab1; // First prefab
    public GameObject prefab2; // Second prefab
    public GameObject prefab3; // Third prefab

    private void Update()
    {
        float playerYPosition = player.transform.position.y;

        if (playerYPosition < -1.260112f)
        {
            EnablePrefab(prefab1);
            DisablePrefab(prefab2);
            DisablePrefab(prefab3);
        }
        else if (playerYPosition >= 3.003024f)
        {
            EnablePrefab(prefab3);
            DisablePrefab(prefab1);
            DisablePrefab(prefab2);
        }
        else
        {
            EnablePrefab(prefab2);
            DisablePrefab(prefab1);
            DisablePrefab(prefab3);
        }
    }

    private void EnablePrefab(GameObject prefab)
    {
        if (!prefab.activeSelf)
        {
            prefab.SetActive(true);
        }
    }

    private void DisablePrefab(GameObject prefab)
    {
        if (prefab.activeSelf)
        {
            prefab.SetActive(false);
        }
    }
}
