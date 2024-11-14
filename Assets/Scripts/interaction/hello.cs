using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hello : MonoBehaviour
{
    [SerializeField]
    private GameObject _codeBox;

    [SerializeField]
    private Camera _camera;

    // Public variable to assign a GameObject in the Unity Inspector
    [SerializeField]
    private GameObject _clickableObject;

    // Start is called before the first frame update
    void Start()
    {
        _codeBox.SetActive(false); // Initially hide the codeBox
    }

    // Update is called once per frame
    void Update()
    {
        UpdateClickHandler();
    }

    private void UpdateClickHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == _clickableObject)
                {
                    _codeBox.SetActive(true); // Activate the codeBox when the assigned GameObject is clicked
                }
            }
        }
    }
}
