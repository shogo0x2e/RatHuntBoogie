using System;
using System.Collections;
using UnityEngine;

public class MicroWave : MonoBehaviour {
    [SerializeField] private GameObject doorContainer;
    [SerializeField] private Light[] spotLights;
    [SerializeField] private GameObject woodenPlate;
    [SerializeField] private AudioClip cookingAudioClip;
    [SerializeField] private AudioClip dingAudioClip;
    [SerializeField] private MicroWaveCollider microWaveCollider;

    private bool isDoorClosed = true;
    private const float slideSpeed = 1.42F;
    private const float totalSlideAmount = 0.54F;
    private float currSlideAmount = 0;

    private const float cookTime = 8F;
    private bool isCooking = false;

    private const float woodenPlateRotateSpeed = 32F;
    private float currWoodenPlateRotation;

    private AudioSource audioSource;

    public void Start() {
        currWoodenPlateRotation = woodenPlate.transform.rotation.y;

        audioSource = GetComponent<AudioSource>();

        TurnLightsOff();
    }

    public void Update() {
        if (isDoorClosed) {
            if (currSlideAmount > 0) {
                currSlideAmount -= slideSpeed * Time.deltaTime;
                currSlideAmount = Math.Max(currSlideAmount, 0);
                UpdateDoorContainerPosition();

                if (currSlideAmount == 0) {
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
            currWoodenPlateRotation += woodenPlateRotateSpeed * Time.deltaTime;
            woodenPlate.transform.rotation = Quaternion.Euler(-180, currWoodenPlateRotation, 0);
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

    private void TurnLightsOff() {
        foreach (Light spotLight in spotLights) {
            spotLight.enabled = false;
        }
    }

    private void StartCooking() {
        isCooking = true;

        Rat currRat = microWaveCollider.GetRatToCook();
        if (currRat != null) {
            currRat.DisableSelf();
            currRat.transform.position = woodenPlate.transform.position;
            currRat.transform.parent = woodenPlate.transform;
        }

        TurnLightOn();
        audioSource.Stop();
        audioSource.clip = cookingAudioClip;
        audioSource.Play();
        StartCoroutine(SetIsCookingFalseAfterDelay());
    }

    private IEnumerator SetIsCookingFalseAfterDelay() {
        yield return new WaitForSeconds(cookTime);
        audioSource.Stop();
        audioSource.clip = dingAudioClip;
        audioSource.Play();
        TurnLightsOff();

        Rat currRat = microWaveCollider.GetRatToCook();
        if (currRat != null) {
            currRat.transform.parent = null;
            currRat.Cook();
        }

        isDoorClosed = false;
        isCooking = false;
    }

    public void Open() {
        if (!isDoorClosed || isCooking) {
            return;
        }

        isDoorClosed = false;
    }

    public void Cook() {
        if (isDoorClosed || isCooking) {
            return;
        }

        isDoorClosed = true;
    }
}
