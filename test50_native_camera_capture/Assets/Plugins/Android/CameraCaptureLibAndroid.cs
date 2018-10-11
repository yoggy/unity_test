using System;
using UnityEngine;

public class CameraCaptureLibAndroid {

	AndroidJavaObject obj;

    public void Check_() {
#if !UNITY_EDITOR && UNITY_ANDROID
        AndroidJNI.AttachCurrentThread();
        if (obj == null) {
            obj = new AndroidJavaObject("net.sabamiso.android.cameracapturelib.CameraCaptureLib");
        }
#endif
    }

	public void setParams(int width, int height)
	{
        Check_();

		// activityを取得
		AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
		AndroidJavaObject currentUnityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"); 

        AndroidJNI.PushLocalFrame(0);
        obj.Call("setParams", new object[] { currentUnityActivity, width, height });
        AndroidJNI.PopLocalFrame(System.IntPtr.Zero);
	}


	public IntPtr createTexturePtr()
	{
        Check_();

		AndroidJNI.PushLocalFrame(0);
		int rv = obj.Call<int>("createTextureId");
		AndroidJNI.PopLocalFrame(System.IntPtr.Zero);

		Debug.Log("createTexturePtr() : rv=" + rv);
		return (IntPtr)rv;
	}

	public void update()
	{
        Check_();
        AndroidJNI.PushLocalFrame(0);
        obj.Call("update");
        AndroidJNI.PopLocalFrame(System.IntPtr.Zero);
	}

	public void dispose()
	{
        Check_();
        AndroidJNI.PushLocalFrame(0);
        obj.Call("dispose");
        AndroidJNI.PopLocalFrame(System.IntPtr.Zero);
	}

	public void startCamera()
	{
        Check_();
        AndroidJNI.PushLocalFrame(0);
        obj.Call("startCamera");
        AndroidJNI.PopLocalFrame(System.IntPtr.Zero);
	}

	public void stopCamera()
	{
        Check_();
        AndroidJNI.PushLocalFrame(0);
        obj.Call("stopCamera");
        AndroidJNI.PopLocalFrame(System.IntPtr.Zero);
	}
}
