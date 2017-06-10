using UnityEngine;

public class PreventSleep : MonoBehaviour
{
    void Start()
    {
        // see also... http://docs.unity3d.com/Documentation/ScriptReference/Screen-sleepTimeout.html
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}