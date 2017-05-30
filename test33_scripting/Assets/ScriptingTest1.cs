using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoonSharp.Interpreter;
using System;

public class ScriptingTest1 : MonoBehaviour {

    [SerializeField]
    InputField inputfield_script;

    [SerializeField]
    InputField inputfield_result;

    void Start()
    {
        inputfield_script.text = "a=1\nb=2\nc=a*a+b*b\nresult=sqrt(c)";
    }

    public void process()
    {
        string header_str = "function f()\n sqrt=math.sqrt\n result=-1\n";
        string footer_str = "\n return result\n end\n return f()\n";

        string script_str = header_str + inputfield_script.text + footer_str;
        Debug.Log(script_str);

        try
        {
            // see also...http://www.moonsharp.org/getting_started.html
            DynValue res = Script.RunString(script_str);

            double rv = res.Number;

            inputfield_result.text = "result=" + rv;
        }
        catch (Exception e)
        {
            inputfield_result.text = "exception occuerd...e=" + e;
        }
    }
}
