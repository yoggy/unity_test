using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {
	
	void Start () {
	}
	
	void Update () {
		gameObject.transform.Rotate(1.0f, 2.0f, 3.0f);
	}
}