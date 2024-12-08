using System;
using System.Collections;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class PlayerLocomotionProvider : MonoBehaviour
{
    public enum Hand
    {
        Left,
        Right
    };
    
    [SerializeField] 
    private GameObject _playerRig;

    [SerializeField] 
    private Transform _cameraTransform;

    [SerializeField] 
    private Transform _controllingPlaneTransform;

    [SerializeField] 
    private float _displacementThreshold = .01f;
    
    [SerializeField] [CanBeNull] 
    private TextMeshProUGUI _debugText;

    [SerializeField] [CanBeNull] 
    private GameObject _debugMarkerPrefab;

    private Vector3 _interactorLeftHandAnchorLocalPosition;
    private Vector3 _interactorRightHandAnchorLocalPosition;

    private Transform _playerRigTransform;
    private Rigidbody _playerRigRigidbody;

    private DateTime _accelarationStartTime;

    private float _RightHandDisplacement;

    private void Start()
    {
        _playerRigTransform = _playerRig.transform;
        _playerRigRigidbody = _playerRig.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _debugText.text = $"RightHandDisplacement: {_RightHandDisplacement}";
    }
    
    public void StartWalkingGrab(Transform interactorTransform, Hand side)
    {
        _playerRigRigidbody.velocity = Vector3.zero;
        
        switch (side)
        {
            case Hand.Left:
                _interactorLeftHandAnchorLocalPosition = 
                    _playerRigTransform.InverseTransformPoint(interactorTransform.position);
                break;
            case Hand.Right:
                _interactorRightHandAnchorLocalPosition = 
                    _playerRigTransform.InverseTransformPoint(interactorTransform.position);
                break;
        }
    }

    public void StepWalkingMovement(Transform interactorTransform, Hand side)
    {
        var interactorWorldPosition = interactorTransform.position;
        
        switch (side)
        {
            case Hand.Left:
                var interactorLeftHandLocalPosition = _playerRigTransform.InverseTransformPoint(interactorWorldPosition);
                var displacementLeft = (interactorLeftHandLocalPosition - _interactorLeftHandAnchorLocalPosition);

                _interactorLeftHandAnchorLocalPosition = interactorLeftHandLocalPosition;

                if (displacementLeft.magnitude > 0.002f)
                {
                    Step(displacementLeft);
                }
                break;
            
            case Hand.Right:
                var interactorRightHandLocalPosition = _playerRigTransform.InverseTransformPoint(interactorWorldPosition);
                var displacementRight = (interactorRightHandLocalPosition - _interactorRightHandAnchorLocalPosition);

                _interactorRightHandAnchorLocalPosition = interactorRightHandLocalPosition;

                _RightHandDisplacement = displacementRight.magnitude;
                if (displacementRight.magnitude > 0.002f)
                {
                    Step(displacementRight);
                }
                else
                {
                    _RightHandDisplacement = 0;
                }
                break;
        }
    }

    private void Step(Vector3 displacement)
    {
        _playerRigTransform.position -= new Vector3(displacement.x, 0, displacement.z) * 10;
    }
}
