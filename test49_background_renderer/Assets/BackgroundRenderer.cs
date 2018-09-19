using UnityEngine;
using System;
using System.Runtime.InteropServices;

[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
public class BackgroundRenderer : MonoBehaviour {

	public Material mat;
	Texture2D background_texture_;

	Color32 [] pixels_;
	private GCHandle pixels_handle_;
	private IntPtr pixels_ptr_;

	void Start() 
	{
		// カメラの背景クリアをSolidColorに設定
		Camera camera = GetComponent<Camera>();
		camera.clearFlags = CameraClearFlags.SolidColor;

		// 背景テクスチャの準備
		background_texture_ = new Texture2D(320, 320, TextureFormat.RGB24, false);
		pixels_ = background_texture_.GetPixels32();
        pixels_handle_ = GCHandle.Alloc(pixels_, GCHandleType.Pinned);
        pixels_ptr_ = pixels_handle_.AddrOfPinnedObject();
	}

	void Update()
	{
		// テスト用にノイズを生成
		for(int y = 0; y < background_texture_.height; ++y) {
			for (int x = 0; x < background_texture_.width; ++x) {				
				pixels_[x + y * background_texture_.width] = new Color32(
					(byte)UnityEngine.Random.Range(0, 255),
					(byte)UnityEngine.Random.Range(0, 255),
					(byte)UnityEngine.Random.Range(0, 255),
					255);
			}
		}

		background_texture_.SetPixels32(pixels_);
        background_texture_.Apply();
	}

	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		mat.SetTexture("_BackgroundTex", background_texture_);
		Graphics.Blit(src, dst, mat);
	}
}
