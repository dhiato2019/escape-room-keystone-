using UnityEngine;

public class FollowRotation : MonoBehaviour
{
    public Transform target;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            // Only change the Y rotation to follow the target's Y rotation
            Vector3 currentRotation = transform.eulerAngles;
            currentRotation.y = target.eulerAngles.y;
            transform.eulerAngles = currentRotation;
        }
    }
}
