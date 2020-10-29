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
