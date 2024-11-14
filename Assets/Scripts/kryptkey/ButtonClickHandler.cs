using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public Button button; // Assign your button in the inspector
    public GameObject objectToDestroy; // Assign the object to destroy
    public GameObject objectToActivate; // Assign the object to activate



    private void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        // Destroy the specified object
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }

        // Activate the specified object
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        PlayerPrefs.SetInt("server_pc_opened", 1);
        PlayerPrefs.Save();
    }
}
