using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    public Button yourButton;  // Assign the button through the inspector


    void Start()
    {

        if (yourButton == null)
        {
            Debug.LogError("Button is not assigned.");
            return;
        }

        // Find the Timer GameObject in the scene and get the Timer script
 

        // Set up the button click listener
        yourButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {

        // Resume the timer if the Timer script is found
    SceneManager.LoadScene("Menu");

    }


}
