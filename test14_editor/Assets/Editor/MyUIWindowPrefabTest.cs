using UnityEngine;
using UnityEditor;
using System.Collections;

public class MyUIWindowPrefabTest : EditorWindow
{
    GameObject targetPrefab;

    [MenuItem("MyUI/MyUIWindow(PrefabTest)")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<MyUIWindowPrefabTest>("MyUIWindow(PrefabTest)");
    }

    void OnGUI()
    {
        GUILayout.Label("Prefabの生成テスト");

        EditorGUILayout.Space();
        GUILayout.Label("生成するPrefabを選択してください");

        EditorGUI.indentLevel++;
        targetPrefab = (GameObject)EditorGUILayout.ObjectField(targetPrefab, typeof(GameObject), false);
        EditorGUI.indentLevel--;

        EditorGUILayout.Space();
        GUILayout.Label("生成する位置を指定してください");
        EditorGUI.indentLevel++;
        EditorGUI.indentLevel--;

        EditorGUILayout.Space();
        if (GUILayout.Button("Prefab生成")) OnButtonPrefab();
    }

    void OnButtonPrimitive()
    {
        var cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
        cube.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
        Undo.RegisterCreatedObjectUndo(cube, "Create Cube");
    }

    void OnButtonPrefab()
    {
#if UNITY_EDITOR
        // Editor中ではこちらを使う
        GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(targetPrefab);
#else
        // 通常の実行時はこちら
        GameObject obj = (GameObject)Instantiate(targetPrefab);
#endif
        obj.name = "prefab";
        obj.transform.position = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), Random.Range(-5, 5));

        Undo.RegisterCreatedObjectUndo(obj, "Create Prefab");
    }
}
