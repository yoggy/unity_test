using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class ShowPolygonCount : MonoBehaviour
{
    static long GetTotalPolygonCount()
    {
        long total_polygon_count = 0;

        // UnityEngine.Object.FindObjectsOfType(）はシーン中に存在するアクティブなgameObjectが列挙対象
        // 非アクティブなオブジェクトや、リソース内も列挙する場合は、UnityEngine.Resources.FindObjectsOfTypeAll()を使用すること
        foreach (MeshFilter filter in UnityEngine.Object.FindObjectsOfType(typeof(MeshFilter)))
        {
            int count = filter.sharedMesh.triangles.Length / 3;
            total_polygon_count += count;
        }
        return total_polygon_count;
    }

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
        long count = GetTotalPolygonCount();

        UnityEditor.Handles.BeginGUI();

        var rect = new Rect(10, SceneView.currentDrawingSceneView.position.height - 60, 200, 40);

        EditorGUI.DrawRect(rect, new Color(0, 0, 0, 0.5f));

        GUIStyle style = new GUIStyle();
        GUIStyleState style_state = new GUIStyleState();
        style.fontSize = 12;
        style.richText = true;
        style.alignment = TextAnchor.MiddleCenter;
        style_state.textColor = Color.white;
        style.normal = style_state;
        GUI.Label(rect, "total_polygon_count = " + count, style);

        UnityEditor.Handles.EndGUI();
    }
#endif
}