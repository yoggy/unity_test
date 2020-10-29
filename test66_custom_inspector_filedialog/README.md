# test66_custom_inspector_filedialog

![img01.png](img01.png)

## code
```
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FileDialogTest : MonoBehaviour
{
    public string filePath;

#if UNITY_EDITOR
    [CustomEditor(typeof(FileDialogTest))]
    public class FileDialogTestInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUIStyle bold_style = new GUIStyle()
            {
                fontSize = EditorStyles.boldFont.fontSize,
                fontStyle = FontStyle.Bold
            };
            EditorGUILayout.LabelField("FileDialogTest", bold_style);

            EditorGUI.indentLevel++;

            // for filePath
            var filePathProp = serializedObject.FindProperty("filePath");
            string filePath = filePathProp.stringValue;

            EditorGUILayout.BeginHorizontal();
            filePath = EditorGUILayout.TextField("filePath", filePath);
            if (GUILayout.Button("...", GUILayout.Width(30)))
            {
                // see also... https://docs.unity3d.com/ja/current/ScriptReference/EditorUtility.OpenFilePanel.html
                string path = EditorUtility.OpenFilePanel("file dialog test", "", "*");

                if (path.Length > 0)
                {
                    filePath = path;
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;

            filePathProp.stringValue = filePath;

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif

}
```

```
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DirDialogTest : MonoBehaviour
{
    public string dirPath;

#if UNITY_EDITOR
    [CustomEditor(typeof(DirDialogTest))]
    public class DirDialogTestInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUIStyle bold_style = new GUIStyle()
            {
                fontSize = EditorStyles.boldFont.fontSize,
                fontStyle = FontStyle.Bold
            };
            EditorGUILayout.LabelField("DirDialogTest", bold_style);

            // for dirPath
            var dirPathProp = serializedObject.FindProperty("dirPath");
            string dirPath = dirPathProp.stringValue;

            EditorGUI.indentLevel++;
            EditorGUILayout.BeginHorizontal();
            dirPath = EditorGUILayout.TextField("dirPath", dirPath);
            if (GUILayout.Button("...", GUILayout.Width(30)))
            {
                // see also... https://docs.unity3d.com/ja/current/ScriptReference/EditorUtility.OpenFolderPanel.html
                string path = EditorUtility.OpenFolderPanel("dir dialog test", "", "*");

                if (path.Length > 0)
                {
                    dirPath = path;
                }
            }
            dirPathProp.stringValue = dirPath;

            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel--;

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
```
