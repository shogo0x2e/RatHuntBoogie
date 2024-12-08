using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class MicroWaveCollider : MonoBehaviour {
    private readonly List<Rat> currRats = new List<Rat>();

    public void OnTriggerEnter(Collider other) {
        if (!AllowTrigger(other)) {
            return;
        }

        Rat currRat = other.gameObject.GetComponent<Rat>();
        currRats.Add(currRat);
    }

    public void OnTriggerExit(Collider other) {
        if (!AllowTrigger(other)) {
            return;
        }

        Rat currRat = other.gameObject.GetComponent<Rat>();
        currRats.Remove(currRat);
    }

    public Rat GetRatToCook() {
        return currRats.Count != 0
            ? currRats[0]
            : null;
    }

    private static bool AllowTrigger(Object other) {
        return other.name.Contains("Rat");
    }
}
