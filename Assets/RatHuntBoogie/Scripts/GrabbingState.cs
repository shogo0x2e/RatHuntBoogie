using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingState : MonoBehaviour
{
    public RatHeadCollision ratHeadCollision;

    public void SetIsGrabbing(bool isGrabbing)
    {
        Debug.Log("SETISGRABBING CALLED");
        ratHeadCollision.isGrabbed = isGrabbing;
    }
}
