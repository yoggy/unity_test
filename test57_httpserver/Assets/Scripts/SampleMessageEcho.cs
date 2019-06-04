using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnBroadcastMessage : UnityEvent<string> { }

public class SampleMessageEcho : MonoBehaviour
{
    public OnBroadcastMessage on_broadcast_message;

    public void OnMessage(string message)
    {
        Debug.Log("SampleMessageEcho.OnOnMessage() message=" + message);
        on_broadcast_message.Invoke(message);
    }
}
