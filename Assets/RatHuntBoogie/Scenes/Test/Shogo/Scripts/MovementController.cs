using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _debugText;
    [SerializeField] private Transform _cameraTransform;
    
    // [SerializeField] private DisplacementDetectable _rightHand;
    [SerializeField] private DisplacementDetectable _leftHand;

    // Update is called once per frame
    void Update()
    {
        // float leftDisplacement = _leftHand.GetDisplacement();
        // float rightDisplacement = _rightHand.GetDisplacement();

        // float displacementMean = ((leftDisplacement + rightDisplacement) / 2);
        // float displacementMean = leftDisplacement;
        //
        // Vector3 movingVector = _cameraTransform.forward * displacementMean;
        // transform.position += new Vector3(movingVector.x, 0, movingVector.z);
        //
        // Debug.Log($"Displacement: {displacementMean}");
        // _debugText.text = transform.position.ToString();
    }
}
