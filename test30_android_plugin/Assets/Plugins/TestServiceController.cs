using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestServiceController : MonoBehaviour {	
	#if UNITY_ANDROID
	// Androidで実行する際、コンストラクタ・デストラクタは使わないようにする
	TestServiceControllerForAndroid controller;

	void Check() {
		if (this.controller == null) {
			this.controller = new TestServiceControllerForAndroid();
		}
	}
	#endif

	public void SetListener(string target_obj, string target_method) {
		Debug.Log ("setListener() : target_obj=" + target_obj + ", target_method=" + target_method);
		#if UNITY_ANDROID
		Check();
		controller.SetListener(target_obj, target_method);
		#endif
	}

	public void OnDestroy() {
		#if UNITY_ANDROID
		if (controller != null) {
			controller.Dispose();
		}
		#endif
	}

	public void TestMethod1() {
		Debug.Log ("testMethod1()");
		#if UNITY_ANDROID
		Check();
		controller.TestMethod1();
		#endif
	}

	public void TestMethod2() {
		Debug.Log ("testMethod2()");
		#if UNITY_ANDROID
		Check();
		controller.TestMethod2();
		#else
		#endif
	}

	public void Start() {
		Debug.Log ("start()");
		#if UNITY_ANDROID
		Check();
		controller.Start();
		#else
		#endif
	}

	public void Stop() {
		Debug.Log ("stop()");
		#if UNITY_ANDROID
		Check();
		controller.Stop();
		#else
		#endif
	}
}
