using System;
using UnityEngine;

public class Plate : MonoBehaviour {
    [SerializeField] private GameObject[] fracturePieces;

    private AudioSource breakSound;

    public void Start() {
        breakSound = fracturePieces[0].GetComponent<AudioSource>();
    }

    public void OnCollisionEnter(Collision other) {
        if (other.gameObject.name.Contains("Paw")) {
            return;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        float colForce = rb.mass * other.relativeVelocity.magnitude;
        if (colForce >= 2.2F) {
            Fracture(other.contacts[0].point, colForce);
        }
    }

    private void Fracture(Vector3 colPoint, float colForce) {
        foreach (GameObject fracturePiece in fracturePieces) {
            fracturePiece.transform.localPosition += transform.localPosition;
            fracturePiece.SetActive(true);
        }

        foreach (GameObject fracturePiece in fracturePieces) {
            Rigidbody fpRb = fracturePiece.GetComponent<Rigidbody>();
            fpRb.AddExplosionForce(colForce * 6F, colPoint, 6F);
        }

        breakSound.Play();

        Destroy(gameObject);
    }
}
