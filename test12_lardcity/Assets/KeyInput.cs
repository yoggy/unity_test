using UnityEngine;
using System.Collections;

public class KeyInput : MonoBehaviour {

	LardCityEmitter emitter;

	void Start () {
		GameObject obj = GameObject.Find ("LardCityEmitter");
		emitter = obj.GetComponent<LardCityEmitter> ();
	}
	
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			emitter.EnableEmit = true;
		} else {
			emitter.EnableEmit = false;
		}
	}
}
