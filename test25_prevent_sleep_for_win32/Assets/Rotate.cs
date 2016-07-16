using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    [SerializeField, Range(-10.0f, 10.0f)]
    float rotDX;

    [SerializeField, Range(-10.0f, 10.0f)]
    float rotDY;

    [SerializeField, Range(-10.0f, 10.0f)]
    float rotDZ;

    void Update () {
        gameObject.transform.Rotate(rotDX, rotDY, rotDZ);
	}
}
