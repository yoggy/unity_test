using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    [Range(-2.0f, 2.0f)]
    public float rotateSpeed = 0.5f;

    public float axisX = 0.0f;
    public float axisY = 1.0f;
    public float axisZ = 0.0f;

    void Start () {
	}
	
	void Update () {
        gameObject.transform.Rotate(new Vector3(axisX, axisY, axisZ), rotateSpeed);
    }
}
