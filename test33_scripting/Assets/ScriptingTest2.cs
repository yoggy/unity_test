using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoonSharp.Interpreter;
using System;

public class ScriptingTest2 : MonoBehaviour {

    [SerializeField]
    InputField inputfield_script;

    [SerializeField]
    InputField inputfield_result;

    void Start()
    {
    }

    public double sqrt(double val)
    {
        return Math.Sqrt(val);
    }

    public void process()
    {
        string header_str = "function f()\nresult = 0";
        string footer_str = "\nreturn result\nend";


        string script_str = header_str + inputfield_script.text + footer_str;

        try
        {
            Script script = new Script();
            script.Globals["sqrt"] = (Func<double, double>)sqrt;

            script.DoString(script_str);
            DynValue res = script.Call(script.Globals["f"]);

            double rv = res.Number;

            inputfield_result.text = "result=" + rv;
        }
        catch (Exception e)
        {
            inputfield_result.text = "exception occuerd...e=" + e;
        }
    }
}
