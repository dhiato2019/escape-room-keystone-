using UnityEngine;

public class DistanceCheck : MonoBehaviour
{
    [System.Serializable]
    public class TargetObjectData
    {
        public GameObject targetObject;  // The public assigned object
        public Animator targetAnimator;   // The Animator of the assigned object
        public string floatParameterName = "opened";  // Name of the float parameter in the Animator
        public AudioSource audioSource;   // The AudioSource component for playing the sound
    }

    public TargetObjectData[] targets;  // Array of target objects and their associated data
    public float checkDistance = 1.524f;  // 5 feet in meters

    void Update()
    {
        // Loop through all target objects
        foreach (var target in targets)
        {
            // Calculate the distance between this object and the target object
            float distance = Vector3.Distance(transform.position, target.targetObject.transform.position);

            // If the distance is less than or equal to the check distance
            if (distance <= checkDistance)
            {
                // Set the float parameter in the Animator to 1 (opened)
                target.targetAnimator.SetFloat(target.floatParameterName, 1f);

                // Play the sound if it hasn't been played yet
                if (target.audioSource && !target.audioSource.isPlaying)
                {
                    target.audioSource.Play();
                }
            }
            else
            {
                // Reset the parameter if the distance is greater than the check distance
                target.targetAnimator.SetFloat(target.floatParameterName, 0f);
            }
        }
    }
}
