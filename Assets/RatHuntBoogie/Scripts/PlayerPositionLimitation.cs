using UnityEngine;

public class PlayerPositionLimitation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        float clampedPosX = Mathf.Clamp(pos.x, -44, 14);
        float clampedPosZ = Mathf.Clamp(pos.z, 14, 44);

        transform.position = new Vector3(clampedPosX, pos.y, clampedPosZ);
    }
}
