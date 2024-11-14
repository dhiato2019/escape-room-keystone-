using UnityEngine;

public class ServerCollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "server"
        if (collision.gameObject.CompareTag("server"))
        {
            // Set PlayerPrefs variable quest to 5


            Debug.Log("Collided with the target object (server)");

            int number = PlayerPrefs.GetInt("server_pc_opened", 0);
            
            Debug.Log("server_pc_opened: " + number);

            if (number == 1)
            {
                // Destroy this object
                Destroy(gameObject);
            PlayerPrefs.SetInt("quest", 5);
            PlayerPrefs.Save();
                // Set the PlayerPrefs variable "dycrypted" to 1
                PlayerPrefs.SetInt("dycrypted", 1);
                PlayerPrefs.Save();

                Debug.Log("Object destroyed, dycrypted set to 1.");
            }
        }
    }
}
