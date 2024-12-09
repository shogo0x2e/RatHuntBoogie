using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVelocityTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 1.0f;
    }
}
