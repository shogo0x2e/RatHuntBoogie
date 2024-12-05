using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryCooker : MonoBehaviour {
    [SerializeField] private GameObject[] fireParticles;

    private bool isOn = false;

    public void Start() {
        TurnFireOff();
    }

    public void OnOff() {
        isOn = !isOn;
        if (isOn) {
            TurnFireOn();
        } else {
            TurnFireOff();
        }
    }

    private void TurnFireOn() {
        foreach (GameObject fireParticle in fireParticles) {
            fireParticle.SetActive(true);
        }
    }

    private void TurnFireOff() {
        foreach (GameObject fireParticle in fireParticles) {
            fireParticle.SetActive(false);
        }
    }

    public bool IsOn() {
        return isOn;
    }
}
