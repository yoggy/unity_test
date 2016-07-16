//
// PreventSleepForWin32.cs - Prevent sleep & display turning off
//
// https://github.com/yoggy/unity_test/test25_prevent_sleep_for_win32/
//
// license:
//     Copyright (c) 2016 yoggy <yoggy0@gmail.com>
//     Released under the MIT license
//     http://opensource.org/licenses/mit-license.php; 
//

using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Collections;

public class PreventSleepForWin32 : MonoBehaviour {

    [FlagsAttribute]
    public enum EXECUTION_STATE : uint
    {
        NULL = 0,
        ES_SYSTEM_REQUIRED  = 0x00000001,
        ES_DISPLAY_REQUIRED = 0x00000002,
        ES_CONTINUOUS       = 0x80000000,
    }

    [DllImport("kernel32.dll")]
    extern static EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

    void Start () {
        // prevent system sleep
        SetThreadExecutionState(EXECUTION_STATE.ES_SYSTEM_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);

        StartCoroutine(loop());
    }

    private IEnumerator loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(10.0f);
            OnTimer();
        }
    }

    void OnTimer()
    {
        // prevent display from turning off
        SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED);
    }

    void OnApplicationQuit()
    {
        // clear ES_SYSTEM_REQUIRED
        SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
    }
}
