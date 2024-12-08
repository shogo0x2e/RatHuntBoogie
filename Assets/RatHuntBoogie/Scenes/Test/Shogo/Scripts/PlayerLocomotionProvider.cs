using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class PlayerLocomotionProvider : MonoBehaviour
{
    [SerializeField] 
    private Transform _cameraTransform;

    [SerializeField] [CanBeNull] 
    private TextMeshProUGUI _debugText;

    private Vector3 _interactorAnchorWorldPosition;

    private void Update()
    {
        _debugText.text = $"Camera: {_cameraTransform.position}";
    }

    public void StartWalkingGrab(Transform interactorTransform)
    {
        _interactorAnchorWorldPosition = interactorTransform.position;
    }

    public void StepWalkingMovement(Transform interactorTransform)
    {

        var interactorWorldPosition = interactorTransform.position;
        var movement = _interactorAnchorWorldPosition - interactorWorldPosition;
        _cameraTransform.position += new Vector3(movement.x, 0, movement.z);

        _interactorAnchorWorldPosition = interactorWorldPosition;
    }
}
