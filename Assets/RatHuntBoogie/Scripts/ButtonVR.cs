using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour {
    [SerializeField] private GameObject bt;
    [SerializeField] private UnityEvent onPress;
    [SerializeField] private UnityEvent onRelease;

    private GameObject srcPresser;
    private AudioSource onPressSound;
    private bool isPressed;

    private Vector3 btBasePosition;
    private Vector3 btPressedPosition;

    public void Start() {
        onPressSound = GetComponent<AudioSource>();
        isPressed = false;

        btBasePosition = transform.localPosition;
        btPressedPosition = new Vector3(0, 0.0056f, 0);
    }

    public void OnTriggerEnter(Collider other) {
        if (!isPressed) {
            bt.transform.localPosition = btPressedPosition;
            srcPresser = other.gameObject;
            onPress.Invoke();
            onPressSound.Play();
        }
    }

    public void OnTriggerExit(Collider other) {
        if (other.gameObject == srcPresser) {
            bt.transform.localPosition = btBasePosition;
            onRelease.Invoke();
            isPressed = false;
        }
    }
}
