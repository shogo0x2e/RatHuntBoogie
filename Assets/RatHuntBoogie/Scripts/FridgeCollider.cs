using System;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using Object = UnityEngine.Object;

public class FridgeCollider : MonoBehaviour {
    private readonly List<Rat> freezingRats = new List<Rat>();

    public void Update() {
        foreach (Rat freezingRat in freezingRats) {
            freezingRat.AddFreezingTimeAcc(Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (!AllowTrigger(other)) {
            return;
        }

        Rat freezingRat = other.gameObject.GetComponent<Rat>();
        freezingRats.Add(freezingRat);
    }

    public void OnTriggerExit(Collider other) {
        if (!AllowTrigger(other)) {
            return;
        }

        Rat freezingRat = other.gameObject.GetComponent<Rat>();
        freezingRat.ResetFreezingTimeAcc();
        freezingRats.Remove(freezingRat);
    }

    private static bool AllowTrigger(Object other) {
        return other.name.Contains("Rat");
    }
}
