using UnityEngine;
using UnityEngine.AI;

public class Rat : MonoBehaviour {
    private NavMeshAgent _agent;

    public GameObject Player;

    public float EnemyDistanceRun = 4.0f;

    private Animator animator;

    private AudioSource audioSource;

    // Random movement settings
    public float randomMoveInterval = 5.0f; // Time between random movements
    public float randomMoveRadius = 10.0f; // Radius for random movement
    private float randomMoveTimer;

    public void Start() {
        _agent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();

        // Initialize the timer with a random interval
        randomMoveTimer = Random.Range(1.0f, 10.0f);
    }

    public void Update() {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < EnemyDistanceRun) {
            // Run away from the player
            if (!audioSource.isPlaying) {
                audioSource.Play();
            }

            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 runDirection = dirToPlayer.normalized;
            Vector3 newPos = transform.position + runDirection * EnemyDistanceRun;

            _agent.SetDestination(newPos);
        } else {
            // Handle random movement
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

        // Smooth animation handling with precise stopping
        if (_agent.remainingDistance > _agent.stoppingDistance || _agent.velocity.magnitude > 0.1f) {
            // If the agent is still far from the target and moving
            animator.SetBool("IsMoving", true);
        } else {
            // Agent is close enough to the destination or stopped
            animator.SetBool("IsMoving", false);
        }
    }


}
