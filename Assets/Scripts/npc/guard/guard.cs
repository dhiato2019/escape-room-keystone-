using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.SceneManagement; // Required for scene management

public class NPCStateMachine : MonoBehaviour
{
    public Transform[] patrolPoints; // Patrol points for NPC
    public float visionRange = 10f; // Distance NPC can see
    public float visionAngle = 100f; // Field of view angle
    public LayerMask playerLayer; // Layer of the player
    public Transform player; // Reference to the player
    public NavMeshAgent agent; // NavMeshAgent for NPC movement
    public Animator animator; // Animator for NPC animations
   public FirstPersonController fpc;
    public float walkSpeed = 1f;
    public float runSpeed = 4f;
    private int currentPatrolIndex;
    private bool isChasingPlayer = false;
    private bool playerInSight = false;
    private bool isYelling = false; // Prevent overlap of yelling

    // Audio for yelling
    public AudioSource yellAudioSource; // AudioSource component for playing yell sound

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentPatrolIndex = 0;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        agent.speed = walkSpeed; // Initialize with walking speed
    }

    void Update()
    {
        Patrol();

        // Ensure the player detection is updated
        playerInSight = DetectPlayer();
        int prefcard = PlayerPrefs.GetInt("prefcard", 0);

        if (playerInSight && prefcard == 1)
        {
            if (!isChasingPlayer && !isYelling)
            {
                StartCoroutine(ChasePlayer());
            }
        }
        else if (isChasingPlayer)
        {
            // Stop chasing if the player is no longer in sight
            StopChasing();
        }

        // Update the animator with the current agent's speed
        UpdateAnimator();
    }

    void Patrol()
    {
        if (agent.remainingDistance < 0.5f && !isChasingPlayer)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    bool DetectPlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer < visionRange)
        {
            float angleBetweenNPCandPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleBetweenNPCandPlayer < visionAngle / 2f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToPlayer, out hit, visionRange))
                {
                    if (hit.collider.CompareTag("Player"))
                    { PlayerPrefs.SetInt("detected",1);
                    PlayerPrefs.Save();
                        return true;
                    }
                    else
                    {
                         PlayerPrefs.SetInt("detected",0);
                    PlayerPrefs.Save();
                    }
                }
            }
            else
            {
            }
        }
        else
        {
        }

        return false;
    }

    IEnumerator ChasePlayer()
    {
        isChasingPlayer = true;
        isYelling = true;

        // Start yelling
        animator.SetBool("yell", true);

        // Play the yelling sound
        if (yellAudioSource != null && !yellAudioSource.isPlaying)
        {
            yellAudioSource.Play();
        }

        agent.speed = 0; 
        yield return new WaitForSeconds(2f);

        // Stop yelling
        animator.SetBool("yell", false);

        // Stop the yelling sound
        if (yellAudioSource != null && yellAudioSource.isPlaying)
        {
            yellAudioSource.Stop();
        }

        isYelling = false;

        // Set run speed and begin chasing
        agent.speed = runSpeed; 
           fpc.lockCursor=false ;

             yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("wasted"); // Load the scene with the specified name

        yield return new WaitForSeconds(2f);

        while (DetectPlayer()) // Continuously detect the player while chasing
        {
            agent.SetDestination(player.position);
            yield return null; // Wait until the next frame
        }

        StopChasing();
    }

    void StopChasing()
    {
        isChasingPlayer = false;
        agent.speed = walkSpeed; // Reset to walk speed
        animator.SetBool("isRunning", false);

        Patrol(); // Go back to patrolling
    }

    void UpdateAnimator()
    {
        float speed = agent.velocity.magnitude;
//        Debug.Log("NPC Speed: " + speed);

        if (speed > 1f)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
        }
        else if (speed > 0.1f)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }

    void OnDrawGizmos()
    {
        // Visualize the NPC's vision cone
        Gizmos.color = Color.yellow;
        Vector3 forward = transform.forward * visionRange;
        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2f, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2f, 0) * forward;

        Gizmos.DrawRay(transform.position, leftBoundary);
        Gizmos.DrawRay(transform.position, rightBoundary);
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
