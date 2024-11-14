using UnityEngine;
using TMPro;
using System.Collections;

public class QuestDisplay : MonoBehaviour
{
    public float typingSpeed = 0.05f; // Delay between each letter
    public TMP_Text textComponent; // Reference to the TMP Text UI
    public GameObject questtrack; // Reference to the TMP Text UI

    public AudioSource audioSource; // Reference to the AudioSource for playing audio
    public GameObject boxintro; // Reference to the TMP Text UI

    // Public arrays for English and French quest audio clips (assignable from the Unity Editor)
    public AudioClip[] quest1EnglishAudioClips;
    public AudioClip[] quest1FrenchAudioClips;
    public AudioClip[] quest2EnglishAudioClips;
    public AudioClip[] quest2FrenchAudioClips;
    public AudioClip[] quest3EnglishAudioClips;
    public AudioClip[] quest3FrenchAudioClips;
    public AudioClip[] quest4EnglishAudioClips;
    public AudioClip[] quest4FrenchAudioClips;
    public AudioClip[] quest5EnglishAudioClips;
    public AudioClip[] quest5FrenchAudioClips;
    public AudioClip[] quest6EnglishAudioClips;
    public AudioClip[] quest6FrenchAudioClips;
    public AudioClip[] quest7EnglishAudioClips;
    public AudioClip[] quest7FrenchAudioClips;
    public AudioClip[] quest8EnglishAudioClips;
    public AudioClip[] quest8FrenchAudioClips;

    // Quest phrases and corresponding audio clips
    private (string[], AudioClip[])[] questData;

    private int lastQuestNumber = 1; // Store the last quest number to detect changes

    void Start()
    {
        // Always initialize the quest to 1 at the start
        PlayerPrefs.SetInt("quest", 1);
        PlayerPrefs.Save();

        lastQuestNumber = PlayerPrefs.GetInt("quest");

        // Initialize questData based on the selected language
        string selectedLanguage = PlayerPrefs.GetString("SelectedLanguage");
        InitializeQuestData(selectedLanguage);

        // Start displaying the phrases for the current quest
        StartCoroutine(ShowQuestPhrases(lastQuestNumber - 1));
        // Start monitoring the quest number for changes
        StartCoroutine(MonitorQuestChange());
    }

    void InitializeQuestData(string selectedLanguage)
    {
        // Initialize questData based on the selected language
        if (selectedLanguage == "fr")
        {
            questData = new (string[], AudioClip[])[] {
                (new string[] { "quest_1_1", "quest_1_2", "quest_1_3" , "quest_1_4" , "quest_1_5" }, quest1FrenchAudioClips),
                (new string[] { "quest_2_1", "quest_2_2", "quest_2_3" }, quest2FrenchAudioClips),
                (new string[] { "quest_3_1", "quest_3_2" }, quest3FrenchAudioClips),
                (new string[] { "quest_4_1", "quest_4_2"}, quest4FrenchAudioClips),
                (new string[] { "quest_5_1", "quest_5_2" }, quest5FrenchAudioClips),
                (new string[] { "quest_6_1", "quest_6_2", "quest_6_3" }, quest6FrenchAudioClips),
                (new string[] { "quest_7_1", "quest_7_2" }, quest7FrenchAudioClips),
                (new string[] { "quest_8_1", "quest_8_2", "quest_8_3" }, quest8FrenchAudioClips)
            };
        }
        else // Default to English
        {
            questData = new (string[], AudioClip[])[] {
                (new string[] { "quest_1_1", "quest_1_2", "quest_1_3" , "quest_1_4" , "quest_1_5"}, quest1EnglishAudioClips),
                (new string[] { "quest_2_1", "quest_2_2", "quest_2_3" }, quest2EnglishAudioClips),
                (new string[] { "quest_3_1", "quest_3_2" }, quest3EnglishAudioClips),
                (new string[] { "quest_4_1", "quest_4_2"}, quest4EnglishAudioClips),
                (new string[] { "quest_5_1", "quest_5_2"}, quest5EnglishAudioClips),
                (new string[] { "quest_6_1", "quest_6_2", "quest_6_3" }, quest6EnglishAudioClips),
                (new string[] { "quest_7_1", "quest_7_2" }, quest7EnglishAudioClips),
                (new string[] { "quest_8_1", "quest_8_2", "quest_8_3" }, quest8EnglishAudioClips)
            };
        }
    }

    IEnumerator MonitorQuestChange()
    {
        while (true)
        {
            int currentQuestNumber = PlayerPrefs.GetInt("quest", 1);

            if (currentQuestNumber < 1 || currentQuestNumber > 8)
            {
                currentQuestNumber = 1;
            }

            if (currentQuestNumber != lastQuestNumber)
            {
                lastQuestNumber = currentQuestNumber;

                yield return StartCoroutine(ShowQuestPhrases(currentQuestNumber - 1));
            }

            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator ShowQuestPhrases(int questIndex)
    {
        questtrack.SetActive(false);
        boxintro.SetActive(true);
        textComponent.text = "";

        string[] keys = questData[questIndex].Item1;
        AudioClip[] clips = questData[questIndex].Item2;

        for (int i = 0; i < keys.Length; i++)
        {
            string phrase = LocalizationManager.Instance.GetLocalizedValue(keys[i]);
            AudioClip clip = clips[i];

            if (clip != null && audioSource != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
                Debug.Log($"Playing audio clip for {keys[i]}");
            }

            yield return StartCoroutine(TypewriterEffect(phrase));

            if (audioSource != null && audioSource.clip != null)
            {
                while (audioSource.isPlaying)
                {
                    yield return null;
                }
            }

            yield return new WaitForSeconds(1f);
        }

        boxintro.SetActive(false);
        questtrack.SetActive(true);
    }

    IEnumerator TypewriterEffect(string phrase)
    {
        textComponent.text = "";
        for (int i = 0; i < phrase.Length; i++)
        {
            textComponent.text = phrase.Substring(0, i + 1);
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}    
