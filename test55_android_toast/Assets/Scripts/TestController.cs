using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public void ShowMessage()
    {
        Toast.Show("test message...t=" + Time.time);
    }
}
