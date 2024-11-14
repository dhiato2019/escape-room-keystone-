using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro

public class QuestTrack : MonoBehaviour
{
    public TMP_Text questTextDisplay; // TextMeshPro component to display the quest text
    private int questNumber;
public GameObject questTrack ;
    private void Start()
    {
        questNumber = PlayerPrefs.GetInt("quest");
        DisplayQuestLines();
    }

    private void Update()
    {
        int newQuestNumber = PlayerPrefs.GetInt("quest");

        // Check if the quest number has changed
        if (newQuestNumber != questNumber)
        {
            questNumber = newQuestNumber;
            questTrack.gameObject.SetActive(false); // Use this to deactivate the entire GameObject

            DisplayQuestLines();

            // Deactivate this GameObject or component
            // Alternatively, you can disable just the QuestTrack component if needed:
            // this.enabled = false;
        }
    }

    public void DisplayQuestLines()
    {
        string selectedLanguage = PlayerPrefs.GetString("SelectedLanguage");
        Dictionary<string, string> questLines = new Dictionary<string, string>();

        // Load quest lines from the localization manager
        string key = "quest" + questNumber;
        Debug.Log(key);
        string localizedValue = LocalizationManager.Instance.GetLocalizedValue(key);

        if (!string.IsNullOrEmpty(localizedValue))
        {
            questLines[key] = localizedValue;
        }

        // Update the TMP text component to display the quests in the selected language
        string displayText = "";
        foreach (var questLine in questLines)
        {
            displayText += questLine.Value + "\n"; // Append each quest line to the display text
        }

        // Set the text to the TMP_Text component
        if (questTextDisplay != null)
        {
            questTextDisplay.text = displayText;
        }
    }
}
