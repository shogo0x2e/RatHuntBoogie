using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Rat : MonoBehaviour {
    private Animator animator;
    private AudioSource audioSource;
    private NavMeshAgent _agent;
    public GameObject Player;
    public float EnemyDistanceRun = 4.0f;
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

    [SerializeField] private bool canMove = true;

    [SerializeField] private GameObject[] modelParts;
    private Collider ratCollider;
    private Rigidbody rigidBody;

    private const float fryCookTime = 6F;
    private float fryCookTimeAcc = 0;

    [SerializeField] private GameObject iceCube;

    [SerializeField] private GameObject iceCubeColliderHolder;

    private const float freezingTime = 6F;
    private float freezingTimeAcc = 0;

    [SerializeField] private GameObject cookedLights;

    [SerializeField] private GameObject eyes;

    public void Start() {
        _agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        randomMoveTimer = Random.Range(1.0f, 10.0f);
        jumpTimer = Random.Range(jumpIntervalMin, jumpIntervalMax);

        ratCollider = GetComponent<Collider>();
        rigidBody = GetComponent<Rigidbody>();

        Physics.IgnoreCollision(ratCollider, iceCubeColliderHolder.GetComponent<Collider>());

        SetCanMove(canMove);
    }

    public void Update() {
        if (canMove) {
            UpdateMovements();
        }
    }

    private void UpdateMovements() {
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

    public void SetCanMove(bool value) {
        canMove = value;
        _agent.enabled = value;
    }

    private void ReEnableSelf() {
        ratCollider.enabled = true;
        EnableRigidBody(true);
    }

    public void DisableSelf() {
        DisableAnimation();
        // ratCollider.enabled = false;
        EnableRigidBody(false);
    }

    public void AddFryCookTimeAcc(float value) {
        fryCookTimeAcc += value;
        if (!IsCooked() && fryCookTimeAcc >= fryCookTime) {
            Cook();
        }
    }

    public void ResetFryCookTimeAcc() {
        fryCookTimeAcc = 0;
    }

    public void Cook() {
        if (IsFrozen()) {
            RemoveFreeze(false);
            ResetFryCookTimeAcc();
            return;
        }

        DisableAnimation();
        cookedLights.SetActive(true);
        SetLayerRecursively(gameObject);
        ReEnableSelf();
    }

    private bool IsCooked() {
        return cookedLights.activeSelf;
    }

    public void AddFreezingTimeAcc(float value) {
        freezingTimeAcc += value;
        if (!IsFrozen() && freezingTimeAcc >= freezingTime) {
            Freeze();
        }
    }

    public void ResetFreezingTimeAcc() {
        freezingTimeAcc = 0;
    }

    private void Freeze() {
        DisableSelf();

        // foreach (GameObject modelPart in modelParts) {
        //     modelPart.transform.parent = iceCubePositionHolder.transform;
        // }
        iceCube.transform.parent = null;
        transform.parent = iceCubeColliderHolder.transform;

        iceCube.SetActive(true);
    }

    private void RemoveFreeze(bool reEnable) {
        if (reEnable) {
            ReEnableSelf();
        }

        // foreach (GameObject modelPart in modelParts) {
        //     modelPart.transform.parent = transform;
        // }
        transform.parent = null;
        iceCube.transform.parent = transform;

        iceCube.SetActive(false);
    }

    private bool IsFrozen() {
        return iceCube.activeSelf;
    }

    private void EnableRigidBody(bool doEnable) {
        rigidBody.isKinematic = !doEnable;
        // rigidBody.detectCollisions = doEnable;
    }

    public void DisableAnimation() {
        animator.enabled = false;
    }

    public void HideEyes() {
        eyes.SetActive(false);
    }

    private static void SetLayerRecursively(GameObject currObj) {
        if (currObj.name.Contains("IceCube")) {
            return;
        }

        currObj.layer = 6;

        foreach (Transform child in currObj.transform) {
            SetLayerRecursively(child.gameObject);
        }
    }
}
