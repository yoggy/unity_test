using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamedPipeSampleController : MonoBehaviour
{
    public Button buttonLaunchClient;
    public InputField inputReceivedMessage;
    public NamedPipeReceiver namedPipeReceiver;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnClickLaunchClient()
    {
        Debug.Log("OnClickLaunchClient()");

    }

    public void OnClickClear()
    {
        Debug.Log("OnClickClear()");
        inputReceivedMessage.text = "";
    }

    public void OnReceiveMessage(string message)
    {
        Debug.Log($"OnReceiveMessage() message={message}");

        if (inputReceivedMessage != null)
        {
            inputReceivedMessage.text = message;
        }
    }
}
