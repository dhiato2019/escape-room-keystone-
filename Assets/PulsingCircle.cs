using System.Collections;
using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    // Variables for configuring the pulse effect
    public float pulseDuration = 1.0f; // Time it takes to complete one pulse cycle
    public float maxScale = 1.5f; // Maximum scale the object reaches during the pulse
    public float minScale = 0.8f; // Minimum scale the object reaches during the pulse
    private Vector3 initialScale; // Initial scale of the object

    // Flag to control if the pulsing effect is enabled
    public bool pulseEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        // Save the original scale of the object
        initialScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the pulse effect is enabled
        if (pulseEnabled)
        {
            // Calculate the scale factor based on a sine wave pattern
            float scale = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(Time.time * Mathf.PI / pulseDuration) + 1) / 2);

            // Apply the calculated scale to the object
            transform.localScale = new Vector3(initialScale.x * scale, initialScale.y * scale, initialScale.z);
        }
    }

    // Optional method to enable or disable the pulse effect
    public void TogglePulse(bool enable)
    {
        pulseEnabled = enable;
        if (!enable)
        {
            // Reset the scale to its original size when pulsing is disabled
            transform.localScale = initialScale;
        }
    }
}
