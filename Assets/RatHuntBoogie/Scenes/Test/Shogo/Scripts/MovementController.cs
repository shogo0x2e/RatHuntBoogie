using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // [SerializeField] private DisplacementDetectable _rightHand;
    [SerializeField] private DisplacementDetectable _leftHand;

    // Update is called once per frame
    void Update()
    {
        float leftDisplacement = _leftHand.GetDisplacement();
        // float rightDisplacement = _rightHand.GetDisplacement();

        // float displacementMean = ((leftDisplacement + rightDisplacement) / 2);
        float displacementMean = leftDisplacement * 10;

        transform.position += transform.up * displacementMean;
        Debug.Log($"Displacement: {displacementMean}");
    }
}
