using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkableInteractor : MonoBehaviour
{
    [SerializeField] 
    private PlayerLocomotionProvider _locomotionProvider;

    private bool _isGrabbing = false;
    private Material _material;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        _locomotionProvider.StartWalkingGrab(transform);
        _isGrabbing = true;
    }

    private void Update()
    {
        _material.color = _isGrabbing ? Color.green : Color.gray;
        if (_isGrabbing)
        {
            _locomotionProvider.StepWalkingMovement(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isGrabbing = false;
    }
}
