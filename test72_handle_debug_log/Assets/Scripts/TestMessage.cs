using System;
using System.Collections;
using UnityEngine;

public class TestMessage : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("OutputLog");
    }

    IEnumerator OutputLog()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            DateTime dt = DateTime.Now;
            Debug.Log(dt.ToString("yyyy/MM/dd  HH:mm:ss"));
        }
    }
}
