using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NamedPipeReceiver : MonoBehaviour
{
    public string pipeName;
    public UnityEvent<string> OnReceive;

    void Start()
    {

    }

    void Update()
    {

    }
}
