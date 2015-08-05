using UnityEngine;
using System.Collections;

public class Blink : MonoBehaviour {

	Material material;

	void Start () {
		Renderer renderer = GetComponent<Renderer> ();
		material = renderer.material;
	}
	
	void Update () {
		if (!OscJack.OscMaster.HasData ("/audio/attack")) {
			material.color = Color.white;
			return;
		}

		material.color = Color.red;

		OscJack.OscMaster.Remove ("/audio/attack");
	}
}
