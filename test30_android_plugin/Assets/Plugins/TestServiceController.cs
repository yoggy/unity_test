using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestServiceController : MonoBehaviour {	
	#if UNITY_ANDROID && !UNITY_EDITOR
	// Androidで実行する際、コンストラクタ・デストラクタは使わないようにする
	TestServiceControllerForAndroid controller;

	void Check() {
		if (this.controller == null) {
			this.controller = new TestServiceControllerForAndroid();
		}
	}
	#elif UNITY_EDITOR
	// UnityEditor上での実行
	#endif

	public void SetListener(string target_obj, string target_method) {
		Debug.Log ("setListener() : target_obj=" + target_obj + ", target_method=" + target_method);
		#if UNITY_ANDROID && !UNITY_EDITOR
		Check();
		controller.SetListener(target_obj, target_method);
		#endif
	}

	public void OnDestroy() {
		#if UNITY_ANDROID && !UNITY_EDITOR
		if (controller != null) {
			controller.Dispose();
		}
		#endif
	}

	public void TestMethod1() {
		Debug.Log ("testMethod1()");
		#if UNITY_ANDROID && !UNITY_EDITOR
		Check();
		controller.TestMethod1();
		#endif
	}

	public void TestMethod2() {
		Debug.Log ("testMethod2()");
		#if UNITY_ANDROID && !UNITY_EDITOR
		Check();
		controller.TestMethod2();
		#else
		#endif
	}

	public void Start() {
		Debug.Log ("start()");
		#if UNITY_ANDROID && !UNITY_EDITOR
		Check();
		controller.Start();
		#else
		#endif
	}

	public void Stop() {
		Debug.Log ("stop()");
		#if UNITY_ANDROID && !UNITY_EDITOR
		Check();
		controller.Stop();
		#else
		#endif
	}

	void Update() {
		TestMethod2 ();
	}
}
