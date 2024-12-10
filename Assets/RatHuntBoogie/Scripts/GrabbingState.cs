using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbingState : MonoBehaviour {
    public RatHeadCollision ratHeadCollision;

    public void SetIsGrabbing(bool isGrabbing) {
        ratHeadCollision.isGrabbed = isGrabbing;
    }
}
