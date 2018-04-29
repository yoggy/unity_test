using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthBufferRenderer : MonoBehaviour {

    private RenderTexture texture_rgb;
    private RenderTexture texture_depth;

    [SerializeField]
    Material material;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, material);
    }
}
