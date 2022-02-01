using System;
using System.IO;
using System.IO.Pipes;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Events;

public class NamedPipeReceiver : MonoBehaviour
{
    public string pipeName;
    public UnityEvent<string> OnReceive;

    NamedPipeServerStream pipe;
    StreamReader sr;

    public void OpenPipe()
    {
        ClosePipe();

        if (String.IsNullOrEmpty(pipeName))
        {
            Debug.LogError("pipeName is empty...!!!");
            return;
        }

        Debug.Log($"OpenPipe() : pipeName={pipeName}");
        var pipe = new NamedPipeServerStream(pipeName, PipeDirection.In);

        Task nowait = OpenPipeAsync();
    }

    public void ClosePipe()
    {
        Debug.Log("ClosePipe()");

        if (sr != null)
        {
            sr.Close();
            sr = null;
        }

        if (pipe != null)
        {
            pipe.Close();
            pipe = null;
        }
    }

    async Task OpenPipeAsync()
    {
        Debug.Log("OpenPipeAsync()");

        await Task.Run(() =>
        {
            Debug.Log("Start WaitForConnection()");
            pipe.WaitForConnection();
            Debug.Log("Finish WaitForConnection()");

            if (pipe.IsConnected == false)
            {
                Debug.LogError($"pipe.IsConnected == false");
                ClosePipe();
                return;
            }
            Debug.Log($"pipe.IsConnected == true");

            StreamReader sr = new StreamReader(pipe);

            while (true)
            {
                string message = sr.ReadLine();
                if (!String.IsNullOrEmpty(message))
                {
                    Debug.Log($"sr.ReadLine() message={message}");
                }

                if (pipe.IsConnected == false)
                {
                    break;
                }
            }

            ClosePipe();
        });
    }
}
