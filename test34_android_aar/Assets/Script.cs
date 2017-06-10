using System;
using UnityEngine;
using UnityEngine.UI;


public class Script : MonoBehaviour, IAccelerometerEvent
{
    [SerializeField]
    Text TextX;

    [SerializeField]
    Text TextY;

    [SerializeField]
    Text TextZ;

    [SerializeField]
    Camera MainCamera;

    AndroidJavaObject accelerometer;
    AccelerometerEventListener event_listener;

    void Awake()
    {
        InitJavaObject();
    }

    void Start()
    {
        CallStart();
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus == true)
        {
            CallStop();
        }
        else
        {
            CallStart();
        }
    }

    void OnApplicationQuit()
    {
        CallStop();

        accelerometer.Dispose();
        accelerometer = null;
    }

    void InitJavaObject()
    {
        event_listener = new AccelerometerEventListener(gameObject);

        AndroidJNI.AttachCurrentThread();
        AndroidJNI.PushLocalFrame(0);

        //
        // AndroidでUnityアプリを実行すると、UnityPlayerというActivityが使用される。
        // このクラスのstaticメンバ変数のUnityPlayer.currentActivityに、
        // 現在表示中のActivityのインスタンスが格納される
        //
        // AndroidJavaClassや、AndroidJavaObjectを使用した後は、
        // using()構文を使用するなど、すぐにDispose()すること。
        //
        using (AndroidJavaClass unity_activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject activity = unity_activity.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                accelerometer = new AndroidJavaObject("net.sabamiso.android.test34_android_aar_module.Accelerometer", new object[] { activity });
                accelerometer.Call("setAccelerometerEventListener", new object[] { event_listener });
            }
        }

        AndroidJNI.PopLocalFrame(System.IntPtr.Zero);
    }

    void CallStart()
    {
        AndroidJNI.AttachCurrentThread();
        AndroidJNI.PushLocalFrame(0);

        accelerometer.Call("start");

        AndroidJNI.PopLocalFrame(System.IntPtr.Zero);
    }

    void CallStop()
    {
        AndroidJNI.AttachCurrentThread();
        AndroidJNI.PushLocalFrame(0);

        accelerometer.Call("stop");

        AndroidJNI.PopLocalFrame(System.IntPtr.Zero);
    }

    public void OnAccelerometer(double x, double y, double z)
    {
        //Debug.Log(string.Format("{0}, {1}, {2}", x, y, z));

        TextX.text = string.Format("{0:f2}", x);
        TextY.text = string.Format("{0:f2}", y);
        TextZ.text = string.Format("{0:f2}", z);

        if (MainCamera != null)
        {
            MainCamera.backgroundColor = new Color((float)x / 10.0f, (float)y / 10.0f, (float)z / 10.0f);
        }
    }
}
