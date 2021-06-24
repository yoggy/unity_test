using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// see also...
// Unity - Scripting API: Application.logMessageReceived https://docs.unity3d.com/ScriptReference/Application-logMessageReceived.html
public class HandleDebugLog : MonoBehaviour
{
    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        AppendText(logString);
        UpdateText();
    }

    public Text textDebugLog;
    public int lines = 15;
    public List<string> messages = new List<string>();

    void AppendText(string msg)
    {
        messages.Add(msg);
        if (messages.Count > lines)
        {
            messages.RemoveAt(0);
        }
    }

    void UpdateText()
    {
        if (textDebugLog == null) return;

        string msg = "";
        for (int i = 0; i < messages.Count; ++i)
        {
            msg += messages[i];
            if (i < messages.Count - 1)
            {
                msg += "\n";
            }
        }
        textDebugLog.text = msg;
    }
}
