using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDelay : MonoBehaviour {

    [SerializeField]
    Text text;

    void Start()
    {
        SetValue(0);
    }

    public void SetValue(float t)
    {
        string msg = string.Format("delay t={0}(ms)", (int)t);
        text.text = msg;
    }
}
