using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpableInteractor : MonoBehaviour
{
    [SerializeField] 
    private JumpLocomotion _jumpLocomotion;
    private Material _material;

    private void Start()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Floor")) return;
        
        _material.color = Color.green;
        _jumpLocomotion.Jump();
    }
}
