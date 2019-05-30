using System;
using System.IO;
using UnityEngine;
using UnityEditor;

//
// SceneViewScreenShot can be used with 2019.1 or later.
//
public class SceneViewScreenShot : MonoBehaviour
{
    static bool fire_capture = false;

    static void OnSceneGUI(SceneView sceneView)
    {
        Handles.BeginGUI();
        GUILayout.Window(1, new Rect(3, 20, 200, 100), OnWindowGUI, "SceneView ScreenShot");
        Handles.EndGUI();
    }

    static void OnWindowGUI(int id)
    {
        if (GUILayout.Button("take screenshot"))
        {
            fire_capture = true;
        }
    }

    void Start()
    {
        SceneView.onSceneGUIDelegate += OnSceneGUI;

        var camera = UnityEditor.SceneView.lastActiveSceneView.camera;
        Camera.onPostRender += OnSceneCameraPostRender;
    }

    void OnDestroy()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        Camera.onPostRender -= OnSceneCameraPostRender;
    }

    public void OnSceneCameraPostRender(Camera camera)
    {
        if (camera.gameObject.name == "SceneCamera")
        {
            if (fire_capture == true)
            {
                TakeSceneViewScreenShot(camera);
                fire_capture = false;
            }
        }
    }

    static void TakeSceneViewScreenShot(Camera camera)
    {
        var render_tex = camera.activeTexture;
        int w = render_tex.width;
        int h = render_tex.height;

        Texture2D tex = new Texture2D(w, h, TextureFormat.RGB24, false);

        var backup_render_tex = RenderTexture.active;
        RenderTexture.active = render_tex;
        tex.ReadPixels(new Rect(0, 0, w, h), 0, 0, false);
        tex.Apply();

        byte[] bytes = ImageConversion.EncodeToJPG(tex, 90);

        var filename = DateTime.Now.ToString("yyyyMMddHHmmss");
        var path = Application.streamingAssetsPath + Path.DirectorySeparatorChar + filename + ".jpg";
        Debug.Log(path);

        File.WriteAllBytes(path, bytes);

        Destroy(tex);

        RenderTexture.active = backup_render_tex;
    }
}
