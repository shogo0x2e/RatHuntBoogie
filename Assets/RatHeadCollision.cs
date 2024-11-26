using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHeadCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject _bloodEffect;

    public bool isGrabbed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isGrabbed && other.CompareTag("Head"))
        {
            EatObject();
        }
        
    }

    private void EatObject()
    {
        // Implement the logic for "eating" the object
        // For example, play a sound, animation, or update score
        _bloodEffect.SetActive(true);
        

        // Destroy the object (simulate being "eaten")
        Destroy(gameObject, 1f);
    }



}
