using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResizeTexture : MonoBehaviour {

    WebCamTexture web_cam_texture;
    Texture2D resized_texture;
    public RawImage raw_image;

    private IEnumerator Start() {
        if (WebCamTexture.devices.Length == 0)
        {
            Debug.LogError("ERROR: WebCamTexture.devices.Length == 0");
            yield break;
        }

        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.LogFormat("ERROR: !Application.HasUserAuthorization(UserAuthorization.WebCam)");
            yield break;
        }

        WebCamDevice dev = WebCamTexture.devices[0];
        Debug.Log("open camera : dev.name=" + dev.name);
        web_cam_texture = new WebCamTexture(dev.name, 640, 480);
        web_cam_texture.Play();

        resized_texture = new Texture2D(16, 16, TextureFormat.RGB24, false);
        resized_texture.filterMode = FilterMode.Point;
        raw_image.texture = resized_texture;
    }

    void Update () {
        if (web_cam_texture == null) return;

        web_cam_texture.filterMode = FilterMode.Point;

        // サイズの異なるRenderTextureを用意して、そこへTexture2DをBlit()することでリサイズ
        RenderTexture tmp_rt = RenderTexture.GetTemporary(resized_texture.width, resized_texture.height);
        Graphics.Blit(web_cam_texture, tmp_rt);

        // RenderTextureの内容をTexture2Dへ反映する
        var current_rt = RenderTexture.active;
        RenderTexture.active = tmp_rt;

        resized_texture.ReadPixels(new Rect(0, 0, resized_texture.width, resized_texture.height), 0, 0);
        resized_texture.Apply(); // Textureの内容を更新したときは必ずApply()を実行すること

        RenderTexture.ReleaseTemporary(tmp_rt);
        RenderTexture.active = current_rt;
    }
}
