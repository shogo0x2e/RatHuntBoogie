using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHeightWithPawInputData : MonoBehaviour
{
    [SerializeField] 
    private PawInputData _pawInputData;

    private void Awake()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, _pawInputData.tableHeight, pos.z);
    }
}
