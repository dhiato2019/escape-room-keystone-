using UnityEngine;

public class ChangeMaterialOnQuest : MonoBehaviour
{
    public Material blackMaterial; // Drag the black material into this field in the inspector
    private Renderer objectRenderer;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>(); // Get the renderer of the attached object
        CheckQuestValue(); // Call the function to check the quest value on start
    }

    void Update()
    {
        CheckQuestValue(); // Continuously check the quest value in Update
    }

    void CheckQuestValue()
    {
        int questValue = PlayerPrefs.GetInt("quest", 0); // Get the quest value from PlayerPrefs, default to 0

        if (questValue == 2)
        {
            objectRenderer.material = blackMaterial; // Change material to black
        }
    }
}
