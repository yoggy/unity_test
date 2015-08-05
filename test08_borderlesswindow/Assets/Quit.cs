using UnityEngine;
using System.Collections;

public class Quit : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
	}
}
