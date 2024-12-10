using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHeadCollision : MonoBehaviour {
    [SerializeField] private GameObject _bloodEffect;

    private Rat rat;
    public bool isGrabbed = false;
    private bool isDead = false;

    public void Start() {
        rat = GetComponent<Rat>();
    }

    private void OnTriggerEnter(Collider other) {
        if (isGrabbed && other.CompareTag("Head")) {
            EatObject();
        }
    }

    private void EatObject() {
        if (isDead) {
            return;
        }

        rat.SetCanMove(false);
        rat.DisableAnimation();
        _bloodEffect.SetActive(true);
        StartCoroutine(DisableBloodEffectAfterDelay());
        Watch.GetInstance().AddScore(200);

        isDead = true;
    }

    private IEnumerator DisableBloodEffectAfterDelay() {
        yield return new WaitForSeconds(1.6F);
        _bloodEffect.SetActive(false);
    }
}
