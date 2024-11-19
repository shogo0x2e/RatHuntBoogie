using System;
using System.Collections;
using UnityEngine;

public class Door : ButtonObject {
    [SerializeField] private GameObject doorContainer;
    [SerializeField] private GameObject glassChild;
    [SerializeField] private float slideSpeed = 1.6F;
    [SerializeField] private float delayAfterClose = 4.2F;

    private float totalSlideAmount;
    private bool isSliding = false;
    private float currSlideAmount = 0;
    private Coroutine slidingCoroutine = null;

    public void Start() {
        totalSlideAmount = glassChild.transform.localScale.x;
    }

    public void Update() {
        if (isSliding) {
            if (currSlideAmount < totalSlideAmount) {
                currSlideAmount += slideSpeed * Time.deltaTime;
                currSlideAmount = Math.Min(currSlideAmount, totalSlideAmount);
                UpdateDoorContainerPosition();
            }
        } else {
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
