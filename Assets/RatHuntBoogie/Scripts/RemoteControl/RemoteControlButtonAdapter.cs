using UnityEngine;

public class RemoteControlButtonAdapter : MonoBehaviour {
    [SerializeField] private RemoteControlButton remoteControlButton;

    public void OnTriggerEnter(Collider other) {
        remoteControlButton.OnTriggerEnterX(other);
    }

    public void OnTriggerExit(Collider other) {
        remoteControlButton.OnTriggerExitX(other);
    }
}
