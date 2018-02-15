using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DebugText : MonoBehaviour {
    Vector3[] vs = new Vector3[] {
        new Vector3(-0.6f, -0.6f, -0.6f),
        new Vector3( 0.6f, -0.6f, -0.6f),
        new Vector3( 0.6f,  0.6f, -0.6f),
        new Vector3(-0.6f,  0.6f, -0.6f),
        new Vector3(-0.6f, -0.6f,  0.6f),
        new Vector3( 0.6f, -0.6f,  0.6f),
        new Vector3( 0.6f,  0.6f,  0.6f),
        new Vector3(-0.6f,  0.6f,  0.6f),
    };

#if UNITY_EDITOR
    private void Start()
    {
        UnityEditor.SceneView.onSceneGUIDelegate += OnSceneView;
    }

    void OnDestroy()
    {
        UnityEditor.SceneView.onSceneGUIDelegate -= OnSceneView;
    }

    void OnSceneView(UnityEditor.SceneView sceneView)
    {
        for (int i = 0; i < vs.Length; ++i)
        {
            DrawIndex(i, vs[i]);
        }
    }

    void DrawIndex(int idx, Vector3 v)
    {
        var sceneCamera = UnityEditor.SceneView.currentDrawingSceneView.camera;

        var local_p = transform.rotation * v;

        var world_p = sceneCamera.WorldToScreenPoint(transform.position + local_p);

        UnityEditor.Handles.BeginGUI();

        var rect = new Rect(world_p.x - 5, UnityEditor.SceneView.currentDrawingSceneView.position.height - world_p.y - 20, 20, 10);

        GUIStyle style = new GUIStyle();
        GUIStyleState style_state = new GUIStyleState();
        style.fontSize = 12;
        style.richText = true;
        style_state.textColor = Color.green;
        style.normal = style_state;
        GUI.Label(rect, "" + idx, style);
 
        UnityEditor.Handles.EndGUI();
    }
#endif
}
