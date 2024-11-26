using UnityEngine;

public class DebugManager : MonoBehaviour
{
    public bool VisualizeWalkingGestureRaycast;
    
    private static DebugManager _instance;

    public static DebugManager GetInstance()
    {
        return _instance;
    }
        
    private void Awake()
    {
        _instance = this;
    }
}
