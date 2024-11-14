using UnityEngine;

public class TrackMovement : MonoBehaviour
{
    // Allow assigning the GameObject to track in the editor
    public GameObject objectToTrack;
    public int variable ;
    private Vector3 lastPosition; // To store the object's last position
    public float movementThreshold = 0.5f; // Movement threshold (0.5 units by default)

    void Start()
    {
        // If no object is assigned in the editor, default to tracking the current GameObject (self)
        if (objectToTrack == null)
        {
            objectToTrack = this.gameObject;
        }

        // Initialize the last position to the object's current position
        lastPosition = objectToTrack.transform.position;

        // Initialize the PlayerPrefs "variable" to 0 if not already set
        PlayerPrefs.SetInt("variable", 0); // 0 means the object has not yet moved
        PlayerPrefs.Save();
    }

    void Update()
    {
        // Early exit if the object has already been moved (i.e., "variable" is set to 1)
        if (PlayerPrefs.GetInt("variable") == 1)
        {
            return; // Exit early as the movement has already been recorded
        }

        // Calculate the distance moved since the last frame
        float distanceMoved = Vector3.Distance(objectToTrack.transform.position, lastPosition);

        // Check if the object has moved more than the threshold
        if (distanceMoved > movementThreshold)
        {
            // Perform the action - set PlayerPrefs "quest" to 3 and mark the event as occurred
            PlayerPrefs.SetInt("quest", variable);
            PlayerPrefs.SetInt("variable", 1); // Mark that the object has moved

            PlayerPrefs.Save(); // Save PlayerPrefs to disk

            // Update lastPosition to the current position after the first movement is detected
            lastPosition = objectToTrack.transform.position;
        }
    }
}
