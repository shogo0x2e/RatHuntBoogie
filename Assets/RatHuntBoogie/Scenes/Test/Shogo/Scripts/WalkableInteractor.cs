using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WalkableInteractor : MonoBehaviour
{
    [SerializeField]
    private PlayerLocomotionProvider _locomotionProvider;

    [SerializeField]
    private PlayerLocomotionProvider.Hand _handSide;

    private bool _isGrabbing = false;
    private Material _material;
    private AudioSource _catTouchingSound;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
        _catTouchingSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Floor")) return;
        _locomotionProvider.StartWalkingGrab(transform, _handSide);
        _isGrabbing = true;
        
        _catTouchingSound.Play();
    }

    private void FixedUpdate()
    {
        _material.color = _isGrabbing ? Color.green : Color.gray;
        if (_isGrabbing)
        {
            _locomotionProvider.StepWalkingMovement(transform, _handSide);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Floor")) return;
        _isGrabbing = false;
    }
}
