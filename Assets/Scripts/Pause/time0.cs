using UnityEngine;

public class CustomTimeScaleObject : MonoBehaviour
{
    private float objectTimeScale = 1f;  // Custom time scale for this object

    private void Start()
    {
        // Set global time scale to 0 when the game starts
        Time.timeScale = 0f;
    }

    private void Update()
    {
        // Manually update object behavior independent of global time scale
        // For example, move or perform actions as if time scale is 1 for this object
        CustomUpdate();
    }

    // This function simulates object-specific behavior based on a custom time scale
    private void CustomUpdate()
    {
        // Example of moving the object with custom time scale (ignoring global time scale)
        float customDeltaTime = objectTimeScale * Time.unscaledDeltaTime;

        // Perform your custom updates for this object, for example, moving it
        // transform.Translate(Vector3.forward * customDeltaTime * speed);

        // Replace the above comment with your own behavior
    }

    private void OnDestroy()
    {
        // Restore the global time scale to 1 when the object is destroyed
        Time.timeScale = 1f;
    }
}
