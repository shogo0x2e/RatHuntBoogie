using System;
using UnityEngine;
using UnityEngine.Events;

public class RemoteControlButton : MonoBehaviour {
    [SerializeField] private UnityEvent onPress;

    private GameObject srcPresser;
    private AudioSource onPressSound;
    private bool isPressed = false;

    private Vector3 btBasePosition;
    private Vector3 btPressedPosition;

    public void Start() {
        onPressSound = GetComponent<AudioSource>();

        btBasePosition = transform.localPosition;
        btPressedPosition = new Vector3(btBasePosition.x, 0.02F, btBasePosition.z);
    }

    public void OnTriggerEnter(Collider other) {
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

    public void OnTriggerExit(Collider other) {
        if (other.gameObject != srcPresser) {
            return;
        }

        transform.localPosition = btBasePosition;
        // TODO: Add release sound

        isPressed = false;
    }
}
