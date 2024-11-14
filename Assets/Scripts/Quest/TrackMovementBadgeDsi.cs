using UnityEngine;

public class TrackMovementBadgeDSI : MonoBehaviour
{
    private Vector3 lastPosition; // To store the object's last position
    public float movementThreshold = 0.5f; // Movement threshold (0.5 units by default)

    void Start()
    {
        // Initialize the last position to the object's current position
        lastPosition = transform.position;
        PlayerPrefs.SetInt("badge", 1);
        PlayerPrefs.Save();
    }

    void Update()
    {
        // Calculate the distance moved since the last frame
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);

        // Check if the object has moved more than the threshold
        if (distanceMoved > movementThreshold)
        {
            PlayerPrefs.SetInt("badge", 1);
            PlayerPrefs.Save(); // Save the PlayerPrefs to disk

            // Check if the badge is 1 and if the quest is not already 4
            if (PlayerPrefs.GetInt("usb") == 1 && PlayerPrefs.GetInt("quest") != 4)
            {
                PlayerPrefs.SetInt("quest", 4);
                PlayerPrefs.Save(); // Save the PlayerPrefs to disk
                Debug.Log("Quest updated to 4.");
            }

            // Update the last position to the current position
            lastPosition = transform.position;
        }
    }
}
