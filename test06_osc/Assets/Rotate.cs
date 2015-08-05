using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Time.deltaTime * 100, Time.deltaTime * 200, Time.deltaTime * 300);	
	}
}
