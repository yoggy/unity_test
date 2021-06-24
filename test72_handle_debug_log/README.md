# test72_handle_debug_log

```
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
        .
        .
        .
```