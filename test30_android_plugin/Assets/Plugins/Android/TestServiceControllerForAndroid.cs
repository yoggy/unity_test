using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ANDROID && !UNITY_EDITOR
public class TestServiceControllerForAndroid {
	AndroidJavaObject controller;
	bool is_init = false;

	public void Check() {
		AndroidJNI.AttachCurrentThread();
		if (is_init == false) {
			controller = new AndroidJavaObject("net.sabamiso.android.androidplugintest.TestServiceController");
			is_init = true;
		}
	}

	public void Dispose() {
		AndroidJNI.DetachCurrentThread();
	}

	public void SetListener(string target_obj, string target_method) {
		Check ();
		AndroidJNI.PushLocalFrame(0);
		controller.Call ("setListener", target_obj, target_method);
		AndroidJNI.PopLocalFrame(System.IntPtr.Zero);
	}

	public void TestMethod1() {
		Check ();
		AndroidJNI.PushLocalFrame(0);
		controller.Call("testMethod1");
		AndroidJNI.PopLocalFrame(System.IntPtr.Zero);
	}

	public void TestMethod2() {
		Check ();

		object[] args = new object[2];
		args [0] = 123;
		args [1] = "testtest";

		AndroidJNI.PushLocalFrame(0);
		string rv = controller.Call<string> ("testMethod2", args);
		AndroidJNI.PopLocalFrame(System.IntPtr.Zero);

		Debug.Log(rv);
	}

	public void Start() {
		Check ();
		AndroidJNI.PushLocalFrame(0);
		controller.Call ("start");
		AndroidJNI.PopLocalFrame(System.IntPtr.Zero);
	}

	public void Stop() {
		Check ();
		AndroidJNI.PushLocalFrame(0);
		controller.Call ("stop");
		AndroidJNI.PopLocalFrame(System.IntPtr.Zero);
	}
}
#endif