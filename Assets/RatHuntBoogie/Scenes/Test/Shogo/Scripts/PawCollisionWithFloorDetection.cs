using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawCollisionWithFloorDetection : DisplacementDetectable
{
    private float _displacement;
    private Vector3 _previousContactPoint = Vector3.zero;
    private Material _colliderMaterial;

    public override float GetDisplacement()
    {
        float d = _displacement;
        _displacement = 0;
        return d;
    }

    private void Start()
    {
        Debug.Log("MeshRenderer started");
        
        var meshRenderer = GetComponent<MeshRenderer>();
        if (!meshRenderer) Debug.LogWarning("MeshRenderer not found");
        
        _colliderMaterial = meshRenderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            Debug.Log($"MeshRenderer entered {other.gameObject.name}");
            _colliderMaterial.color = Color.red;
            _previousContactPoint = other.ClosestPoint(transform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            Vector3 currentPosition = other.ClosestPoint(transform.position);
            Vector3 dVector = currentPosition - _previousContactPoint;
            
            _displacement += dVector.magnitude;
            _previousContactPoint = currentPosition;
            
            Debug.Log($"MeshRenderer staying {_displacement}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            Debug.Log($"MeshRenderer exited {other.gameObject.name}");
            _colliderMaterial.color = Color.gray;
        }
    }
}
