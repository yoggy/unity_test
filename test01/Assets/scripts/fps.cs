using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fps : MonoBehaviour {

	public Text fpsText;

	int count = 0;
	float dulation = 3.0f;
	float t;

	void Start () {
		reset ();
	}

	void reset() {
		count = 0;
		t = dulation;
	}

	void Update () {
		t -= Time.deltaTime;
		count++;

		if (t <= 0) {
			fpsText.text = "fps:" + count / dulation;
			reset ();
		}
	}
}
