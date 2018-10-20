using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EncodeToJPGTest : MonoBehaviour {

    public RawImage raw_image;
    public Text quality_text;

    WebCamTexture web_cam_texture;
    Texture2D resized_texture;
    Texture2D jpeg_texture;

    int quality = 75; // default

    private IEnumerator Start()
    {
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
        web_cam_texture.filterMode = FilterMode.Point;
        web_cam_texture.Play();

        resized_texture = new Texture2D(64, 64, TextureFormat.RGB24, false);
        resized_texture.filterMode = FilterMode.Point;
        jpeg_texture = new Texture2D(64, 64, TextureFormat.RGB24, false);
        jpeg_texture.filterMode = FilterMode.Point;

        raw_image.texture = jpeg_texture;
    }

    void Update()
    {
        if (web_cam_texture == null) return;
        ResizeTexture();

        // Texture2D -> JPEG(byte[])
        byte[] jpeg_data = resized_texture.EncodeToJPG(quality);

        // JPEG(byte[]) -> Texture2D
        jpeg_texture.LoadImage(jpeg_data);
        jpeg_texture.Apply();
    }

    void ResizeTexture()
    {
        RenderTexture tmp_rt = RenderTexture.GetTemporary(resized_texture.width, resized_texture.height);
        Graphics.Blit(web_cam_texture, tmp_rt);

        var current_rt = RenderTexture.active;
        RenderTexture.active = tmp_rt;

        resized_texture.ReadPixels(new Rect(0, 0, resized_texture.width, resized_texture.height), 0, 0);
        resized_texture.Apply();

        RenderTexture.ReleaseTemporary(tmp_rt);
        RenderTexture.active = current_rt;
    }

    public void OnValueChanged(float val)
    {
        quality = (int)val;
        quality_text.text = "jpeg_quality=" + quality;
    }
}
