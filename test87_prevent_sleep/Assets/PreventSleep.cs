using UnityEngine;

public class PreventSleep : MonoBehaviour
{
    // https://docs.unity3d.com/ScriptReference/Screen-sleepTimeout.html
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
