using UnityEngine;

public class Door : ButtonObject {
    public override void OnButtonPressed() {
        Debug.Log("Hello");
    }

    public override void OnButtonReleased() { }
}
