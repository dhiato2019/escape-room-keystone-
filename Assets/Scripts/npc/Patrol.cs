using UnityEngine;
using UnityEngine.AI;

public class PatrolWithNavMesh : MonoBehaviour
{
    public Transform targetA; // First patrol point
    public Transform targetB; // Second patrol point
    public Animator animator; // Reference to the Animator component
    public float raycastDistance = 1.0f; // Raycast stopping distance
    public float rotationSpeed = 5f; // Speed for smooth rotation

    private NavMeshAgent navAgent; // Reference to the NavMeshAgent component
    private Vector3 currentTarget; // Current target to move towards

    void Start()
    {
        // Get the NavMeshAgent component
        navAgent = GetComponent<NavMeshAgent>();

        // Disable auto rotation to handle it manually
        navAgent.updateRotation = false;

        // Start patrolling towards the first target
        currentTarget = targetA.position;

        // Set the initial destination for the NavMeshAgent
        navAgent.SetDestination(currentTarget);
    }

    void Update()
    {
        // Handle smooth rotation towards the target
        SmoothRotateTowardsDestination();

        // Check if the agent is close to the target using a raycast
        if (IsNearTarget())
        {
            SwitchTarget();
        }

        // Update the animator with the agent's speed
        float speed = navAgent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }

    void SwitchTarget()
    {
        // Switch the target between targetA and targetB
        if (currentTarget == targetA.position)
        {
            currentTarget = targetB.position;
        }
        else
        {
            currentTarget = targetA.position;
        }

        // Set the new destination for the NavMeshAgent
        navAgent.SetDestination(currentTarget);
    }

    // Smooth rotation towards the movement direction
    void SmoothRotateTowardsDestination()
    {
        if (navAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            Vector3 direction = navAgent.velocity.normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    // Check if the agent is near the target using a raycast
    bool IsNearTarget()
    {
        Ray ray = new Ray(transform.position, navAgent.destination - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            if (hit.collider.transform.position == currentTarget)
            {
                return true; // Agent is close to the target
            }
        }
        return false;
    }
}
