using UnityEngine;
using System.Collections;

public class CountUp : MonoBehaviour {

    public TextMesh targetTextMesh;

    void Start () {
	
	}
	
	void Update () {
        if (targetTextMesh == null) return;


        targetTextMesh.text = string.Format("t={0:f2}", Time.time);
    }
}
