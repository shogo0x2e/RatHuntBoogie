using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHeadCollision : MonoBehaviour {
    [SerializeField] private GameObject _bloodEffect;
    [SerializeField] private GameObject eatSoundGameObject;

    private Rat rat;
    public bool isGrabbed = false;
    private bool isDead = false;
    private AudioSource eatSound;

    public void Start() {
        rat = GetComponent<Rat>();
        eatSound = eatSoundGameObject.GetComponent<AudioSource>();
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
        rat.HideEyes();
        rat.DisableAnimation();
        eatSound.Play();
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
