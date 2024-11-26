using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingGestureDetection : RaycastCollisionDetection
{
    [SerializeField] 
    private Transform _player;

    [SerializeField] 
    private Transform _cameraTransform;
    
    private Vector3 _previousPosition;
    
    protected override void OnRaycastEnter(RaycastHit hit)
    {
        base.OnRaycastEnter(hit);
        _previousPosition = hit.point;
        
        Debug.Log($"WalkingGesture::{hit.transform.gameObject.name}");
    }

    protected override void OnRaycastStay(RaycastHit hit)
    {
        base.OnRaycastStay(hit);
        
        // Transform parentTransform = transform.parent.transform;
        // Vector3 parentPos = parentTransform.position;
        
        // paw not to go through the table
        // parentTransform.position = new Vector3(parentPos.x, _pawInputData.tableHeight, parentPos.z);

        Vector3 pawMovingDiff = hit.point - _previousPosition;
        Vector3 playerPosition = _player.transform.position;
        Vector3 toward = playerPosition - new Vector3(pawMovingDiff.x, 0, pawMovingDiff.z);
        
        _player.transform.position = Vector3.Lerp(
            playerPosition, 
            toward, 
            _pawInputData.lowPassFilterFactor
        );
        _previousPosition = hit.point;
    }
}