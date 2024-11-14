using UnityEngine;
using UnityEngine.AI;

public class MoveObjectToTarget : MonoBehaviour
{
    public Transform target; // The target position to move to
    private NavMeshAgent agent; // Reference to the NavMeshAgent

    void Start()
    {
        // Get the NavMeshAgent component attached to this object
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Set the destination of the NavMeshAgent to the target position
        if (target != null)
        {
            agent.SetDestination(target.position);

            // Check if the object has reached the target
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                // If the object has reached the target, disable the NavMeshAgent and this script
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    agent.enabled = false; // Disable NavMeshAgent
                    this.enabled = false;  // Disable this script
                }
            }
        }
    }
}
