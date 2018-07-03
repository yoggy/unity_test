using UnityEngine;

public class ChangeResolution : MonoBehaviour
{
    public void OnButtonTest1()
    {
        Debug.Log("ChangeResolution.OnButtonTest1()");
        Screen.SetResolution(300, 300, false); // screen width, height, fullscreen flag
    }

    public void OnButtonTest2()
    {
        Debug.Log("ChangeResolution.OnButtonTest2()");
        Screen.SetResolution(400, 400, false); // screen width, height, fullscreen flag
    }

    public void OnButtonTest3()
    {
        Debug.Log("ChangeResolution.OnButtonTest3()");
        Screen.SetResolution(600, 600, false); // screen width, height, fullscreen flag
    }
}
