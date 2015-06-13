using UnityEngine;
using System.Collections;

public class WebCam : MonoBehaviour {
	WebCamTexture webcamTexture;

	void Start () {
		WebCamDevice [] devices = WebCamTexture.devices;

		for (int i = 0; i < devices.Length; ++i) {
			Debug.Log ("i=" + i + ", name=" + devices[i].name);
		}

		if (devices.Length > 0)
		{
			webcamTexture = new WebCamTexture(640, 480, 30);

			Renderer rendrer = gameObject.GetComponent<Renderer>();
			rendrer.material.mainTexture = webcamTexture;
			webcamTexture.Play();
		}
		else
		{
			Debug.Log("Webカメラが検出できませんでした");
			return;
		}
	}
}