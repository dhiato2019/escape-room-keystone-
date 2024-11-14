using UnityEngine;

public class QuestUpdaterOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object's name is "USBKey_Red"
        if (collision.gameObject.name == "USBKey_Red")
        {
            // Update the quest PlayerPref to 5
            PlayerPrefs.SetInt("quest", 5);
            PlayerPrefs.Save(); // Ensure the changes are saved immediately
            
            Debug.Log("Quest updated to 5 due to collision with USBKey_Red.");
        }
    }
}
