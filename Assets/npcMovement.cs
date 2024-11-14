using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float stoppingDistance = 0.5f;
    private NavMeshAgent agent;
    private Animator animator;
    private Transform currentTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentTarget = pointA; // Start by moving towards point A
        agent.SetDestination(currentTarget.position);
    }

    void Update()
    {
        // Update animator speed parameter based on agent's velocity
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);

        // Check if NPC has reached the target point
        if (!agent.pathPending && agent.remainingDistance <= stoppingDistance)
        {
            SwitchTarget();

        }
    }

    // Switch between point A and point B
    void SwitchTarget()
    {
        currentTarget = (currentTarget == pointA) ? pointB : pointA;
        agent.SetDestination(currentTarget.position);
    }
}
