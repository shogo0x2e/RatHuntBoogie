using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionIgnore : MonoBehaviour
{
    [SerializeField] 
    private GameObject _pawLeft;

    [SerializeField] 
    private GameObject _pawRight;

    [SerializeField] 
    private GameObject _head;

    private void Start()
    {
        Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), _pawLeft.GetComponent<BoxCollider>());
        Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), _pawRight.GetComponent<BoxCollider>());
        Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), _head.GetComponent<BoxCollider>());
    }
}
