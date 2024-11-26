using UnityEngine;

public class RaycastCollisionDetection : MonoBehaviour
{
    [SerializeField] 
    protected PawInputData _pawInputData;

    private bool _hasCollided = false;

    protected virtual void OnRaycastEnter(RaycastHit hit) { }
    protected virtual void OnRaycastStay(RaycastHit hit) { }
    protected virtual void OnRaycastExit() {}

    private void Update()
    {
        Vector3 thisPosition = transform.position;
        Vector3 direction = Vector3.down;
        Ray ray = new Ray(thisPosition, direction);
        
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, _pawInputData.pawRayLength))
        {
            if (!_hasCollided)
            {
                _hasCollided = true;
                OnRaycastEnter(hit);
            }
            OnRaycastStay(hit);
        }
        else
        {
            if (_hasCollided)
            {
                _hasCollided = false;
                OnRaycastExit();
            }
        }
    }
}
