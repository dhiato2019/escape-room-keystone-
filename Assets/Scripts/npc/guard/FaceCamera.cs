using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    // Reference to the main camera
    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        // Get the reference to the main camera
    }

    // Update is called once per frame
    void Update()
    {
        // Make the object face the camera
        FaceTowardsCamera();
    }

    // Method to rotate the object towards the camera
    void FaceTowardsCamera()
    {
        // Calculate direction from the object to the camera
        Vector3 directionToCamera = mainCamera.transform.position + transform.position;

        // Set y to 0 to only rotate horizontally (optional)
        directionToCamera.y = 0;

        // Create a rotation that faces the direction to the camera
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

        // Apply the rotation to the object
        transform.rotation = targetRotation;
    }
}
