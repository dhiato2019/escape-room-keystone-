using System.Collections;
using UnityEngine;

public class AppearDisappear : MonoBehaviour
{
    // Reference to the object that will appear/disappear
    [SerializeField] private GameObject targetObject;

    // Time to wait before making the object appear and disappear
    [SerializeField] private float appearDelay = 2f;
    [SerializeField] private float disappearDelay = 2f;

    void Start()
    {
        // Start the Coroutine to manage appearance and disappearance
        StartCoroutine(AppearDisappearRoutine());
    }

    IEnumerator AppearDisappearRoutine()
    {
        // Initially make the object invisible
        targetObject.SetActive(false);

        // Wait for appearDelay seconds
        yield return new WaitForSeconds(appearDelay);

        // Make the object appear
        targetObject.SetActive(true);

        // Wait for disappearDelay seconds
        yield return new WaitForSeconds(disappearDelay);

        // Make the object disappear
        targetObject.SetActive(false);
    }
}
