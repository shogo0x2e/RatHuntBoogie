using System.Linq;
using UnityEngine;

public class RemoteControlButtonAdapter : MonoBehaviour {
    [SerializeField] private RemoteControlButton remoteControlButton;

    public void OnTriggerEnter(Collider other) {
        if (!AllowTrigger(other)) {
            return;
        }

        remoteControlButton.OnTriggerEnterX(other);
    }

    public void OnTriggerExit(Collider other) {
        if (!AllowTrigger(other)) {
            return;
        }

        remoteControlButton.OnTriggerExitX(other);
    }

    private static bool AllowTrigger(Object other) {
        return other.name.Contains("Paw");
    }
}
