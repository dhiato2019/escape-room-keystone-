using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChecker : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; // Assign the GameObject to update the tag in the Inspector
    [SerializeField] private string pickupTag = "canPickUp"; // The tag to assign when quest is 2
    private int questValue;
    public int quest;

    private void Start()
    {
        // Start checking for the PlayerPrefs quest value repeatedly
        StartCoroutine(CheckQuestValue());
    }

    // Coroutine to repeatedly check the quest value
    private IEnumerator CheckQuestValue()
    {
        while (true)
        {
            // Get the value of 'quest' from PlayerPrefs
            questValue = PlayerPrefs.GetInt("quest", 1); // Default to 1 if no value is set

            // If quest value is 2, assign the tag to the target object
            if (questValue == quest)
            {
                if (targetObject != null)
                {
                    targetObject.tag = pickupTag;
                    Debug.Log($"Quest value is 2. Assigned tag '{pickupTag}' to {targetObject.name}");
                }
                else
                {
                    Debug.LogError("Target object is not assigned.");
                }
            }

            // Wait for 0.5 seconds before checking again
            yield return new WaitForSeconds(0.5f);
        }
    }
}
