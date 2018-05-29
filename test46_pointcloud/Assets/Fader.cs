using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour {

    Material material;

    void Start () {
        material = GetComponent<Renderer>().material;
    }
	
	void Update () {
        float t = Time.fixedTime;
        float size = 10 * 5 * Mathf.Cos(t*2.0f);
        material.SetFloat("_PointSize", size);
    }
}
