using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    [SerializeField]
    float rx;

    [SerializeField]
    float ry;

    [SerializeField]
    float rz;

	void Start () {
	
	}
	
	void Update () {
		transform.Rotate (rx, ry, rz);
	}
}
