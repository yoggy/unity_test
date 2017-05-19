using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreventSleep : MonoBehaviour {
    void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
