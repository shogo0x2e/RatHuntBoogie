using System;
using System.Collections;
using UnityEngine;

public class Door : ButtonObject {
    [SerializeField] private GameObject doorContainer;
    [SerializeField] private float slideSpeed = 1.6F;
    [SerializeField] private float delayAfterClose = 7.2F;
    [SerializeField] private float totalSlideAmount = 1.5F;

    private bool isSliding = false;
    private float currSlideAmount = 0;
    private Coroutine slidingCoroutine = null;

    private AudioSource slidingSound;

    public void Start() {
        slidingSound = GetComponent<AudioSource>();
    }

    public void Update() {
        if (isSliding) {
            if (currSlideAmount == 0) {
                slidingSound.Play();
            }

            if (currSlideAmount < totalSlideAmount) {
                currSlideAmount += slideSpeed * Time.deltaTime;
                currSlideAmount = Math.Min(currSlideAmount, totalSlideAmount);
                UpdateDoorContainerPosition();
            }
        } else {
            if (currSlideAmount == totalSlideAmount) {
                slidingSound.Play();
            }

            if (currSlideAmount > 0) {
                currSlideAmount -= slideSpeed * Time.deltaTime;
                currSlideAmount = Math.Max(currSlideAmount, 0);
                UpdateDoorContainerPosition();
            }
        }
    }


    private void UpdateDoorContainerPosition() {
        doorContainer.transform.localPosition = currSlideAmount * transform.right;
    }

    public override void OnButtonPressed() {
        CancelSlidingCoroutine();
        isSliding = true;
    }

    public override void OnButtonReleased() {
        slidingCoroutine = StartCoroutine(SetIsSlidingFalseAfterDelay());
    }

    private IEnumerator SetIsSlidingFalseAfterDelay() {
        yield return new WaitForSeconds(delayAfterClose);
        isSliding = false;
        slidingCoroutine = null;
    }

    private void CancelSlidingCoroutine() {
        if (slidingCoroutine != null) {
            StopCoroutine(slidingCoroutine);
            slidingCoroutine = null;
        }
    }
}
