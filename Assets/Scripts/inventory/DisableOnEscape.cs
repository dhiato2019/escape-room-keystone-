using UnityEngine;

public class DisableOnEscape : MonoBehaviour
{
    // Drag and drop the three GameObjects you want to disable in the inspector.
    public GameObject object1; 
    public GameObject object2;
    public GameObject object3;

    void Update()
    {
        // Check if the Escape key is pressed.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Disable all three GameObjects.
            object1.SetActive(false);
            object2.SetActive(false);
            object3.SetActive(false);
        }
    }
}
