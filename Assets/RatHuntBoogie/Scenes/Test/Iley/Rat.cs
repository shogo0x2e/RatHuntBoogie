using UnityEngine;
using UnityEngine.AI;

public class Rat : MonoBehaviour {
    private NavMeshAgent _agent;

    public GameObject Player;

    public float EnemyDistanceRun = 4.0f;

    private Animator animator;

    private AudioSource audioSource;

    // Random movement settings
    public float randomMoveInterval = 5.0f; 
    public float randomMoveRadius = 10.0f; 
    private float randomMoveTimer;

    
    public float jumpIntervalMin = 2.0f; 
    public float jumpIntervalMax = 5.0f; 
    private float jumpTimer;
    public float jumpHeight = 2.0f; 
    public float jumpDuration = 0.5f; 
    private bool isJumping = false;
    private Vector3 jumpStartPosition;
    private float jumpElapsedTime;
    private Vector3 jumpDirection; 

    public void Start() {
        _agent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        randomMoveTimer = Random.Range(1.0f, 10.0f);
        jumpTimer = Random.Range(jumpIntervalMin, jumpIntervalMax);
    }

    public void Update() {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        // Run away from the player
        if (distance < EnemyDistanceRun) {
            
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }

            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 runDirection = dirToPlayer.normalized;
            Vector3 newPos = transform.position + runDirection * EnemyDistanceRun;

            _agent.SetDestination(newPos);

        // Handle random movement
        } else {
            
            randomMoveTimer -= Time.deltaTime;

            if (randomMoveTimer <= 0) {
                Vector3 randomDirection = Random.insideUnitSphere * randomMoveRadius;
                randomDirection += transform.position;

                if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, randomMoveRadius, NavMesh.AllAreas)) {
                    _agent.SetDestination(hit.position);
                }

                // Set a new random time interval for the next move
                randomMoveTimer = Random.Range(1.0f, 10.0f);
            }
        }

        // Handle random jumps
        if (!isJumping) {
            jumpTimer -= Time.deltaTime;

            if (jumpTimer <= 0) {
                isJumping = true;
                jumpStartPosition = transform.position;
                jumpElapsedTime = 0.0f;
                jumpDirection = transform.forward * 1.0f; 
                jumpTimer = Random.Range(jumpIntervalMin, jumpIntervalMax); 

                animator.SetBool("IsJumping", true);
            }
        } else {
            jumpElapsedTime += Time.deltaTime;

            if (jumpElapsedTime <= jumpDuration) {
                float normalizedTime = jumpElapsedTime / jumpDuration;
                float verticalOffset = 4 * jumpHeight * normalizedTime * (1 - normalizedTime); 

                // Move forward during the jump
                transform.position = new Vector3(
                    jumpStartPosition.x + jumpDirection.x * normalizedTime, 
                    jumpStartPosition.y + verticalOffset, // vertical offset
                    jumpStartPosition.z + jumpDirection.z * normalizedTime // forward in the Z direction
                );
            } else {
                isJumping = false;
                transform.position = new Vector3(
                    jumpStartPosition.x + jumpDirection.x, 
                    jumpStartPosition.y,
                    jumpStartPosition.z + jumpDirection.z
                );

                animator.SetBool("IsJumping", false);
            }
        }

        if (_agent.remainingDistance > _agent.stoppingDistance || _agent.velocity.magnitude > 0.1f) {
            animator.SetBool("IsMoving", true);
        } else {
            animator.SetBool("IsMoving", false);
        }
    }
}
