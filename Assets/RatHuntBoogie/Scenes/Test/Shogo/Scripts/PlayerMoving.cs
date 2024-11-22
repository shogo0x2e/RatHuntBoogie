using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector2 movement = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        transform.position += new Vector3(movement.x, 0,  movement.y);
    }
}
