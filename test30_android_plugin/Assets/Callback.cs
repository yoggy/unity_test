using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Callback : MonoBehaviour {

	public TestServiceController test_service_controller;
	public Text text;

	void Start () {
		if (test_service_controller != null) {
			test_service_controller.SetListener(gameObject.name, "OnMessage");
		}
	}

	void OnMessage(string msg) {
		Debug.Log (msg);
		text.text = msg;
	}
}
