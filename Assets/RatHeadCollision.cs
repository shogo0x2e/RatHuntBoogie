using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHeadCollision : MonoBehaviour
{


    public bool isGrabbed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isGrabbed && other.CompareTag("Head")) Debug.Log("grabbed and head");
        
    }



    //public void onCollisionEnter (RatHeadCollision collision)
    //{

    //    if (isGrabbed && collision.gameObject.CompareTag("PlayerHead"))
    //    {
    //        // Call the "Eat" method
    //        EatObject();
    //    }
    //}

    private void EatObject()
    {
        // Implement the logic for "eating" the object
        // For example, play a sound, animation, or update score
        Debug.Log($"{gameObject.name} is eaten!");

        // Destroy the object (simulate being "eaten")
        Destroy(gameObject);
    }



}
