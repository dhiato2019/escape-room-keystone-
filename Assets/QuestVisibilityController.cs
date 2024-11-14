using UnityEngine;

public class QuestVisibilityController : MonoBehaviour
{
    public GameObject targetObjectquest2; // The GameObject to show or hide based on the quest state
    public GameObject targetObjectquest4; // The GameObject to show or hide based on the quest state

    private void Update()
    {
        CheckQuestStatus(); // Check the quest status when the game starts
    }

    public void CheckQuestStatus()
    {
        // Retrieve the quest variable from PlayerPrefs
        int questStatus = PlayerPrefs.GetInt("quest"); // Default value is 0 if not set

        // Check if quest status is 2
        if (questStatus == 2)
        {
            targetObjectquest2.SetActive(true); // Activate the object
        }
        else
        {
            targetObjectquest2.SetActive(false); // Activate the object
        }
                if (questStatus == 4)
        {
            targetObjectquest4.SetActive(true); // Activate the object
 // Activate the object
        }
        else
        {
            targetObjectquest4.SetActive(false); // Activate the object
        }
    }


}
