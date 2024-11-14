using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationSceneSwitcher : MonoBehaviour
{
    // Assign the Animator component from the Editor
    public Animator animator;

    // Name of the animation to check for completion
    public string animationName;

    // Name of the scene to load when animation finishes
    public string sceneToLoad;

    // Flag to prevent multiple scene loads
    private bool sceneSwitched = false;

    void Update()
    {
        // Check if the animation is finished and the scene hasn't been switched yet
        if (IsAnimationFinished() && !sceneSwitched)
        {
            // Load the new scene
            SceneManager.LoadScene(sceneToLoad);

            // Prevent multiple scene loads
            sceneSwitched = true;
        }
    }

    // Function to check if the animation is finished
    private bool IsAnimationFinished()
    {
        // Get the current animator state info from the first layer
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Check if the current animation is the one we're looking for and has finished playing
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1.0f;
    }
}
