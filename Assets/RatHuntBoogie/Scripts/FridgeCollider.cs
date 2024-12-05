using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class FridgeCollider : MonoBehaviour {
    
    
    public void Update() {
        
    }
    
    public void OnTriggerEnter(Collider other) {
        if (!AllowTrigger(other)) {
            return;
        }
    }

    public void OnTriggerExit(Collider other) {
        if (!AllowTrigger(other)) {
            return;
        }
    }

    private static bool AllowTrigger(Object other) {
        return other.name.Contains("Rat");
    }
}
