// see also... https://docs.unity3d.com/jp/540/Manual/editor-EditorWindows.html
using UnityEngine;
using UnityEditor;
using System.Collections;

public class PXWindow : EditorWindow
{
    [MenuItem("PXWindow/PXWindow test")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PXWindow));
    }

    bool is_initialized;
    static int angle;
    Texture[] textures = new Texture[16];

    void loadTextures()
    {
        for (int i = 0; i < textures.Length; ++i)
        {
            string filename = string.Format("{0:D2}", i);
            Debug.Log(filename);
            textures[i] = (Texture)Resources.Load(filename);
            if (textures[i] == null)
            {
                Debug.LogError("Resources.Load() failed...filename=" + filename);
            }
        }
    }


    void CheckInitialize()
    {
        if (is_initialized == false)
        {
            loadTextures();
            is_initialized = true;
        }
    }

    void OnGUI()
    {
        CheckInitialize();

        GUILayout.Label("rotate");

        EditorGUI.indentLevel++;
        using (new EditorGUILayout.HorizontalScope())
        {
            angle = EditorGUILayout.IntSlider("", angle, 0, 40);
        }
        EditorGUI.indentLevel--;

        EditorGUI.indentLevel++;
        using (new EditorGUILayout.HorizontalScope())
        {
            EditorGUI.DrawTextureTransparent(new Rect(10, 50, 200, 200), textures[angle % textures.Length], ScaleMode.ScaleToFit);
        }
    }
}
