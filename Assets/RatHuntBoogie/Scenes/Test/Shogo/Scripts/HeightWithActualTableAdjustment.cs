using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeightWithActualTableAdjustment : MonoBehaviour
{
    [SerializeField] 
    private float _tableHeight = 0.74f;
    
    [SerializeField] 
    private CapsuleCollider _touchingCollider;

    [SerializeField] 
    private Transform _playerRigVR;

    // Update is called once per frame
    void Update()
    {
        var colliderOffset = _touchingCollider.radius / 2;
        var headHeightFromActualFloor = _playerRigVR.InverseTransformPoint(transform.position).y;
        var colliderCenter = _touchingCollider.center;
        
        _touchingCollider.center = new Vector3(
            colliderCenter.x,
            -(headHeightFromActualFloor - _tableHeight - colliderOffset),
            colliderCenter.z
        );
    }
}
