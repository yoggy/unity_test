using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugLogText : MonoBehaviour
{
    Text text;
    int maxLine = 16;

    List<string> logs = new List<string>();

    void Awake()
    {
        // see also...https://docs.unity3d.com/ScriptReference/Application-logMessageReceived.html
        Application.logMessageReceived += HandleLog;

        text = GetComponent<Text>();
        text.text = "";
    }

    void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    public void Clear()
    {
        logs.Clear();
        UpdateText();
    }

    void HandleLog(string condition, string stackTrace, LogType type)
    {
        string t = $"{Time.time:F2}";
        string type_str = type.ToString();

        string str = $"{t} {type_str} {condition}";

        logs.Add(str);
        if (logs.Count > maxLine)
        {
            logs.RemoveAt(0);
        }

        UpdateText();
    }

    void UpdateText()
    {
        string logs_str = "";
        for (int i = 0; i < logs.Count; ++i)
        {
            logs_str += logs[i];
            if (i < logs.Count - 1)
            {
                logs_str += "\n";
            }
        }

        // padding...
        if (logs.Count < maxLine)
        {
            for (int i = 0; i < maxLine - logs.Count; ++i)
            {
                logs_str += "\n";
            }
        }

        text.text = logs_str;

    }
}
