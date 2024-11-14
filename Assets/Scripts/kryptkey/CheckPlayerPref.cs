using UnityEngine;

public class CheckPlayerPref : MonoBehaviour
{
    public GameObject objectToDestroy; // Assign the object to destroy
    public GameObject objectToActivate; // Assign the object to activate
    public string Variable ;

 void Start()
{
    PlayerPrefs.SetInt(Variable , 0);
    PlayerPrefs.Save();
}
    private void Update()
    {
        // Log the current PlayerPrefs value for debugging
        Debug.Log($"Variable value: {PlayerPrefs.GetInt("Variable")}");
    Debug.Log($"quest value: {PlayerPrefs.GetInt("quest")}");

        // Check the PlayerPrefs value continuously
        if (PlayerPrefs.GetInt(Variable) == 1)
        {
            // Destroy the specified object if not null
            if (objectToDestroy != null)
            {
                Debug.Log("Destroying object...");
                objectToDestroy.SetActive(false);
               
            }

            // Activate the specified object if not null
            if (objectToActivate != null)
            {
                Debug.Log("Activating object...");
                objectToActivate.SetActive(true);
            }
        }
    }
}
