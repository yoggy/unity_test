using UnityEngine;
using System.Collections;

public class CameraShader : MonoBehaviour {
	
	public Material material;
	
	void OnRenderImage ( RenderTexture src, RenderTexture dest ) {
		Graphics.Blit(src, dest, material);
	}
}