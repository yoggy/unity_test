using UnityEngine;
using System.Collections;

public class CheckHeight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < -10.0f ) {
			Destroy(gameObject, 0.5f);
		}
	}
}
