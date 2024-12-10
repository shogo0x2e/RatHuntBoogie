using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLocomotion : MonoBehaviour
{
    [SerializeField]
    private Transform _leftHand;

    [SerializeField]
    private Transform _rightHand;

    [SerializeField]
    float _thresholdDistance = 1.0f;

    [SerializeField]
    private GameObject _effect;

    [SerializeField] 
    private PlayerLocomotionProvider _playerLocomotionProvider;

    void Update()
    {
        float distance = Vector3.Distance(_leftHand.position, _rightHand.position);
        if (distance <= _thresholdDistance)
        {
            _playerLocomotionProvider.IsWalkingDisabled = true;
            Vector3 middlePoint = (_leftHand.position + _rightHand.position) / 2;
            _effect.transform.position = middlePoint;
            _effect.SetActive(true);
        }
        else
        {
            _playerLocomotionProvider.IsWalkingDisabled = false;
            _effect.SetActive(false);
        }
    }

    public void Jump()
    {
        _playerLocomotionProvider.Jump();
    }
}
