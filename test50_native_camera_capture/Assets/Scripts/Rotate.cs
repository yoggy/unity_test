using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public float rx;
	public float ry;
	public float rz;

	void Update () {
		gameObject.transform.Rotate(rx, ry, rz);		
	}
}
