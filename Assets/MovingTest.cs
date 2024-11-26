using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTest : MonoBehaviour
{
    void Update()
    {
        var input = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
        transform.position += new Vector3(input.x * .5f, 0, input.y * .5f);
    }
}
