using UnityEngine;
using UnityEngine.AI;

public class Rat : MonoBehaviour {
    private NavMeshAgent _agent;

    public GameObject Player;

    public float EnemyDistanceRun = 4.0f;

    private Animator animator;

    private AudioSource audioSource;

    public void Start() {
        _agent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    public void Update() {
    float distance = Vector3.Distance(transform.position, Player.transform.position);

    if (distance < EnemyDistanceRun) {
        animator.SetBool("IsMoving", true);

        if (!audioSource.isPlaying) {
            audioSource.Play();
        }

        Vector3 dirToPlayer = transform.position - Player.transform.position;

        Vector3 runDirection = dirToPlayer.normalized;

        Vector3 newPos = transform.position + runDirection * EnemyDistanceRun;

        _agent.SetDestination(newPos);
    } else {
        animator.SetBool("IsMoving", false);
    }
}

}
