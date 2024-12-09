using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawCollisionWithFloorDetection : DisplacementDetectable
{
    private Vector2 _displacementOnPlane;
    private Vector2 _contactPoint;
    private Vector2 _previousFrameContactPoint;
    private Material _colliderMaterial;

    public override Vector2 GetDisplacement()
    {
        var d = _displacementOnPlane;
        // _displacementOnPlane = Vector2.zero;
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
            _colliderMaterial.color = Color.red;
            var contactPoint3D = other.ClosestPoint(transform.position);
            
            var contactPoint2D = new Vector2(contactPoint3D.x, contactPoint3D.z);
            _contactPoint = contactPoint2D;
            _previousFrameContactPoint = contactPoint2D;
            Debug.Log("Impl: Enter");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            var currentPosition = transform.position;
            var contactPoint3D = other.ClosestPoint(currentPosition);
            var contactPoint2D = new Vector2(contactPoint3D.x, contactPoint3D.z);
            _displacementOnPlane += (contactPoint2D - _previousFrameContactPoint);

            Debug.Log($"Impl: {contactPoint2D} | {_previousFrameContactPoint}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Floor"))
        {
            _colliderMaterial.color = Color.gray;
            Debug.Log("Impl: Exit");
        }
    }
}
