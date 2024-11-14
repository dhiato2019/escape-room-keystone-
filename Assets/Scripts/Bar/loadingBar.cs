using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using TMPro;

public class LoadingBar : MonoBehaviour
{
    public GameObject bar; // Reference to the loading bar object
    public int time;       // Time to complete the loading animation
    public TextMeshProUGUI messageText; // TMP component to display the "Copy is done" message

    private GameObject usb;
    private bool isAnimating = false; // Flag to track animation state

    void Start()
    {
        // Ensure message is empty at the start
        messageText.text = "";

        // Start the loading animation once
        AnimateBar();
    }

    // Update is called once per frame
    void Update()
    {
        // Remove this if you don't need to check the animation state every frame
    }

    public void AnimateBar()
    {
        if (!isAnimating) // Check if already animating
        {
            isAnimating = true; // Set the flag to true
            // Animate the bar's scale over time
            LeanTween.scaleX(bar, 1, time).setOnComplete(OnLoadingComplete);
        }
    }

    // Callback when loading is complete
    public void OnLoadingComplete()
    {
        // Display the message "Copy is done" using the TextMeshPro component
        messageText.text = "Copy is done";
      
        // Activate the USB object if it is found
       
        // Set the PlayerPref key "quest" to 8
        PlayerPrefs.SetInt("quest", 8);
        PlayerPrefs.Save(); // Ensure PlayerPrefs is saved immediately

        // Optionally reset the animation state if needed
        isAnimating = false; // Reset the flag if you want to allow reanimation later
    }
}
