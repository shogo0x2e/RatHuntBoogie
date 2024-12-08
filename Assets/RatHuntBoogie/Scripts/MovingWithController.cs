using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWithController : MonoBehaviour
{
    void Update()
    {
        Vector2 input = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        transform.position += new Vector3(input.x, 0, input.y) * .3f;
    }
}
