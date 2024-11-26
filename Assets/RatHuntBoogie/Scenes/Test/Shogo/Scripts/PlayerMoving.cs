using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    // [SerializeField]
    // private 
    
    private Vector3 _previousHandPosition;

    void Start()
    {
        if (_previousHandPosition == null)
        {
            
        }
    }
    
    void Update()
    {
        Vector2 movement = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        transform.position += new Vector3(movement.x, 0,  movement.y);
    }
}
