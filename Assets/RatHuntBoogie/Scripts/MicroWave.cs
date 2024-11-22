using System;
using System.Collections;
using UnityEngine;

public class MicroWave : MonoBehaviour {
    [SerializeField] private GameObject doorContainer;
    [SerializeField] private Light[] spotLights;
    [SerializeField] private GameObject woodenPlate;

    private bool isDoorClosed = true;
    private const float slideSpeed = 1.42F;
    private const float totalSlideAmount = 0.54F;
    private float currSlideAmount = 0;

    private const float cookTime = 6F;
    private bool isCooking = false;

    public void Start() {
        TurnLightOff();
    }

    public void Update() {
        if (isDoorClosed) {
            if (currSlideAmount > 0) {
                currSlideAmount -= slideSpeed * Time.deltaTime;
                currSlideAmount = Math.Max(currSlideAmount, 0);
                UpdateDoorContainerPosition();

                if (currSlideAmount == 0) {
                    TurnLightOn();
                    StartCooking();
                }
            }
        } else {
            if (currSlideAmount < totalSlideAmount) {
                currSlideAmount += slideSpeed * Time.deltaTime;
                currSlideAmount = Math.Min(currSlideAmount, totalSlideAmount);
                UpdateDoorContainerPosition();
            }
        }

        if (isCooking) {
            Debug.Log("Hello");
        }
    }

    private void UpdateDoorContainerPosition() {
        doorContainer.transform.localPosition = currSlideAmount * transform.up;
    }

    private void TurnLightOn() {
        foreach (Light spotLight in spotLights) {
            spotLight.enabled = true;
        }
    }

    private void TurnLightOff() {
        foreach (Light spotLight in spotLights) {
            spotLight.enabled = false;
        }
    }

    private void StartCooking() {
        isCooking = true;
        StartCoroutine(SetStartCookingFalseAfterDelay());
    }

    private IEnumerator SetStartCookingFalseAfterDelay() {
        yield return new WaitForSeconds(cookTime);
        isCooking = false;
    }

    public void Open() {
        if (!isDoorClosed) {
            return;
        }

        TurnLightOff();
        isDoorClosed = false;
    }

    public void Cook() {
        if (isDoorClosed) {
            return;
        }

        isDoorClosed = true;
    }
}
