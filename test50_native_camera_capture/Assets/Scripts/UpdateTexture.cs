using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class UpdateTexture : MonoBehaviour {

	public GameObject targetGameObject;
	Texture2D texture;
	Texture2D externalTexture;
	CameraCaptureLibAndroid cameraCapture;

	void Start () {
		cameraCapture = new CameraCaptureLibAndroid();
		int tex_w = 1280, tex_h = 720;
		cameraCapture.setParams(tex_w, tex_h);

		texture = new Texture2D(tex_w, tex_h, TextureFormat.RGBA32, false);
		texture.filterMode = FilterMode.Point;

		IntPtr ptr = cameraCapture.createTexturePtr();
		Debug.Log("texture_id=" + (int)ptr);

		// 注:CreateExternalTexture()内ではglTexImage2D(テクスチャバッファの確保)は行わない
		//    Unity内でどのようなTextureとして扱うかの情報だけを与える。
		externalTexture = Texture2D.CreateExternalTexture (
			tex_w, tex_h, TextureFormat.RGBA32, false, true, ptr);

		// テクスチャを更新する場合は、UpdateExternalTexture()を使用
		texture.UpdateExternalTexture(externalTexture.GetNativeTexturePtr());

		// gameObjectのメインテクスチャを置き換える
		Renderer renderer = targetGameObject.GetComponent<Renderer>();
		renderer.material.color = Color.white;
		renderer.material.mainTexture = texture;

		cameraCapture.startCamera();
	}
	
	void Update () {
		cameraCapture.update();
		texture.UpdateExternalTexture(externalTexture.GetNativeTexturePtr());
	}

	void OnApplicationQuit()
	{
		cameraCapture.stopCamera();
		cameraCapture.dispose();
	}
}