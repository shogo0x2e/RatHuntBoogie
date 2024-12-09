using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingGestureDetection : RaycastCollisionDetection
{
    [SerializeField] 
    private TMPro.TextMeshProUGUI _debugDiffText;
    
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

        Vector3 pawMovingDiff = hit.point - _previousPosition;
        Vector3 playerPosition = _player.transform.position;
        Vector3 toward = playerPosition - new Vector3(pawMovingDiff.x, 0, pawMovingDiff.z);

        _debugDiffText.text = pawMovingDiff.magnitude.ToString();
        
        _player.transform.position = toward;
        _previousPosition = hit.point;
    }
}
