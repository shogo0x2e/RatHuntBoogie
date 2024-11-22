using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour {
    [SerializeField] private GameObject bt;
    [SerializeField] private ButtonObject[] attachedObjects;
    [SerializeField] private UnityEvent onPress;
    [SerializeField] private UnityEvent onRelease;

    private GameObject srcPresser;
    private AudioSource onPressSound;
    private bool isPressed = false;

    private Vector3 btBasePosition;
    private Vector3 btPressedPosition;

    public void Start() {
        onPressSound = GetComponent<AudioSource>();

        btBasePosition = transform.localPosition;
        btPressedPosition = new Vector3(btBasePosition.x, 0.0056F, btBasePosition.x);
    }

    public void OnTriggerEnter(Collider other) {
        if (isPressed) {
            return;
        }

        if (other.GetComponent<Rigidbody>() == null) {
            return;
        }

        bt.transform.localPosition = btPressedPosition;
        srcPresser = other.gameObject;
        onPress.Invoke();
        onPressSound.Play();

        foreach (ButtonObject attachedObject in attachedObjects) {
            attachedObject.OnButtonPressed();
        }

        isPressed = true;
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject != srcPresser) {
            return;
        }

        bt.transform.localPosition = btBasePosition;
        onRelease.Invoke();
        // TODO: Add release sound

        foreach (ButtonObject attachedObject in attachedObjects) {
            attachedObject.OnButtonReleased();
        }

        isPressed = false;
    }
}
