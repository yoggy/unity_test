using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthBufferRenderer : MonoBehaviour {

    [SerializeField]
    Material material;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, material);
    }
}
