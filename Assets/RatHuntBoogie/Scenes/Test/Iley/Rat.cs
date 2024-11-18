using UnityEngine;
using UnityEngine.AI;

public class Rat : MonoBehaviour {
    private NavMeshAgent _agent;

    public GameObject Player;

    public float EnemyDistanceRun = 4.0f;

    private Animator animator;

    public void Start() {
        _agent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();
    }

    public void Update() {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        Debug.Log("Distance:" + distance);

        if (distance < EnemyDistanceRun) {
            animator.SetBool("IsMoving", true);
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position = dirToPlayer;

            _agent.SetDestination(newPos);
        } else {
            animator.SetBool("IsMoving", false);
        }
    }
}
