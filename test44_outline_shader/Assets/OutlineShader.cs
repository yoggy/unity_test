using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineShader : MonoBehaviour {

    [SerializeField]
    bool enable;

    [SerializeField]
    Material material;

    RenderTexture outline_buffer;

    void Start()
    {
        GetComponent<Camera>().depthTextureMode |= DepthTextureMode.Depth;
    }

    private void OnDestroy()
    {
        if (outline_buffer != null)
        {
            outline_buffer.Release();
            outline_buffer = null;
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (enable == false)
        {
            Graphics.Blit(src, dst);
            return;
        }

        if (outline_buffer == null || outline_buffer.width != src.width || outline_buffer.height != src.height)
        {
            if (outline_buffer != null)
            {
                outline_buffer.Release();
                outline_buffer = null;
            }

            outline_buffer = new RenderTexture(src.width, src.height, src.depth, RenderTextureFormat.ARGB32);
        }

        Graphics.Blit(src, outline_buffer, material);
        Graphics.Blit(outline_buffer, dst);
    }
}
