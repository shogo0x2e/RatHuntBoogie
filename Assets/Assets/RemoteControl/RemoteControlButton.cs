using UnityEngine;
using UnityEngine.Events;

public class RemoteControlButton : MonoBehaviour {
    [SerializeField] private UnityEvent onPress;
    [SerializeField] private Vector3 pressedTargetPosition;

    private GameObject srcPresser;
    private AudioSource onPressSound;
    private bool isPressed = false;

    private Vector3 btBasePosition;
    private Vector3 btPressedPosition;

    public void Start() {
        onPressSound = GetComponent<AudioSource>();

        btBasePosition = transform.localPosition;

        float btPressedY = btBasePosition.y - transform.localScale.y * (90F / 100F);
        btPressedPosition = pressedTargetPosition != Vector3.zero
            ? pressedTargetPosition
            : new Vector3(btBasePosition.x, btPressedY, btBasePosition.z);
    }

    public void OnTriggerEnterX(Collider other) {
        if (isPressed) {
            return;
        }

        if (other.GetComponent<Rigidbody>() == null) {
            return;
        }

        transform.localPosition = btPressedPosition;
        srcPresser = other.gameObject;
        onPress.Invoke();
        onPressSound.Play();

        isPressed = true;
    }

    public void OnTriggerExitX(Collider other) {
        if (other.gameObject != srcPresser) {
            return;
        }

        transform.localPosition = btBasePosition;
        // TODO: Add release sound

        isPressed = false;
    }
}
