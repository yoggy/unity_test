using UnityEngine;
using System.Collections;

public class IntervalTimer : MonoBehaviour {

	Renderer renderer;
	Material material;
	float t;

	void Start () {
		renderer = GetComponent<Renderer> ();
		material = renderer.material;
		t = 0.0f;
	}
	
	void Update () {
		t += Time.deltaTime;
		if (t > 1.0f) {
			t = 0;
			material.color = Color.red;
			Debug.Log ("Trigger");
			Logger.Log("time=" + Time.fixedTime);
		} else {
			material.color = Color.white;
		}
	}
}
