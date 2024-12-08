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
    private float _displacementThreshold = .01f;

    [SerializeField]
    private float _noiseThreshold = .002f;

    [SerializeField]
    private float _movementMultiplier = 10f;

    [SerializeField, CanBeNull]
    private TextMeshProUGUI _debugText;

    // [SerializeField] [CanBeNull] 
    // private TextMeshProUGUI _debugText;
    //
    // [SerializeField] [CanBeNull] 
    // private GameObject _debugMarkerPrefab;

    private Vector3 _interactorLeftHandAnchorLocalPosition;
    private Vector3 _interactorRightHandAnchorLocalPosition;

    private Transform _playerRigTransform;
    private Rigidbody _playerRigRigidbody;

    private void Start()
    {
        _playerRigTransform = _playerRig.transform;
        _playerRigRigidbody = _playerRig.GetComponent<Rigidbody>();
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

                if (displacementLeft.magnitude > _noiseThreshold)
                {
                    Step(displacementLeft);
                }
                break;

            case Hand.Right:
                var interactorRightHandLocalPosition = _playerRigTransform.InverseTransformPoint(interactorWorldPosition);
                var displacementRight = (interactorRightHandLocalPosition - _interactorRightHandAnchorLocalPosition);

                _interactorRightHandAnchorLocalPosition = interactorRightHandLocalPosition;

                if (displacementRight.magnitude > _noiseThreshold)
                {
                    Step(displacementRight);
                }
                break;
        }
    }

    private void Step(Vector3 displacement)
    {
        Vector3 cameraForward = _cameraTransform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        // displacement is negative because the walking gesture is opposite to the camera's forward direction
        Vector3 projectedDisplacement = Vector3.ProjectOnPlane(-displacement, Vector3.up); 9 =
        Vector3 forwardDisplacement = Vector3.Project(projectedDisplacement, cameraForward);

        // Ensure the player does not move backward
        if (Vector3.Dot(forwardDisplacement, cameraForward) > 0)
        {
            _playerRigTransform.position += forwardDisplacement * _movementMultiplier;
        }
    }
}
