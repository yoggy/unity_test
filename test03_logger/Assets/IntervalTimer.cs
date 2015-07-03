using UnityEngine;
using System.Collections;

public class IntervalTimer : MonoBehaviour {

	Renderer r;
	Material m;
	float t;

	void Start () {
		r = GetComponent<Renderer> ();
		m = r.material;
		t = 0.0f;
	}
	
	void Update () {
		t += Time.deltaTime;
		if (t > 1.0f) {
			t = 0;
			m.color = Color.red;
			Debug.Log ("Trigger");
			Logger.Log("frameCount=" + Time.frameCount);
		} else {
			m.color = Color.white;
		}
	}
}
