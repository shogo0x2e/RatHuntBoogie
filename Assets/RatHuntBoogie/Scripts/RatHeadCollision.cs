using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHeadCollision : MonoBehaviour {
    [SerializeField] private GameObject _bloodEffect;

    public bool isGrabbed = false;

    private bool isDead = false;

    private void OnTriggerEnter(Collider other) {
        if (isGrabbed && other.CompareTag("Head")) {
            EatObject();
        }
    }

    private void EatObject() {
        if (isDead) {
            return;
        }

        _bloodEffect.SetActive(true);
        StartCoroutine(DisableBloodEffectAfterDelay());
        Watch.GetInstance().AddScore(200);

        isDead = true;
    }

    private IEnumerator DisableBloodEffectAfterDelay() {
        yield return new WaitForSeconds(1F);
        _bloodEffect.SetActive(false);
    }
}
