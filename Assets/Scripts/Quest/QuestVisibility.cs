using UnityEngine;

public class QuestVisibility : MonoBehaviour
{
    public GameObject targetObject; // Assign this from the Inspector
    public int questValue;
    private bool flag = false; // A flag to control when to stop checking

    void Start()
    {
        // Start method is empty; initialization happens here if needed
    }

    void Update()
    {
        if (!flag) // Only run this block if flag is false
        {
            UpdateVisibility(); // Call the visibility update method
        } 
    }

    void UpdateVisibility()
    {
        // Get the quest value from PlayerPrefs, default to 0 if not found
        questValue = PlayerPrefs.GetInt("quest", 0);

        // If quest value is 2 or greater, make the target object visible
        if (questValue >= 2)
        {   
            flag = true; // Set the flag to true to stop further updates
            targetObject.SetActive(true); // Make the target object visible
        }
        else
        { 
            targetObject.SetActive(false); // Make the target object invisible if quest value is less than 2
        }
    }
}
