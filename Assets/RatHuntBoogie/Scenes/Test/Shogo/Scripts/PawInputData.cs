using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Object/PawInputData")]
public class PawInputData : ScriptableObject
{
    [Range(0.01f, 1.0f)]
    public float lowPassFilterFactor;

    public float tableHeight;
    public float pawRayLength;
}
