using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastExample : MonoBehaviour
{
    [SerializeField] 
    private Transform _floorTransform;

    [SerializeField] 
    private Transform _playerTransform;

    private Vector3 _previousPawPosition = Vector3.zero;
    private bool _hasPawsLanded = false;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 thisPosition = transform.position;
        Vector3 direction = (_floorTransform.position - thisPosition).normalized;
        var ray = new Ray(thisPosition, direction);

        RaycastHit hit;
        Color debugRayColor = Color.red;
        if (Physics.Raycast(ray, out hit, .1f))
        {
            debugRayColor = Color.green;
            if (!_hasPawsLanded)
            {
                _hasPawsLanded = true;
                _previousPawPosition = hit.point;
            }
            _playerTransform.position += (hit.point - _previousPawPosition);
        }
        else
        {
            _hasPawsLanded = false;
        }
        
        Debug.DrawRay(ray.origin, ray.direction * .1f, debugRayColor);

    }
}
