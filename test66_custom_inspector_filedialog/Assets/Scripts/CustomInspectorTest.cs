using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CustomInspectorTest : MonoBehaviour
{
    public int intValue;
    public string stringValue;

#if UNITY_EDITOR
    [CustomEditor(typeof(CustomInspectorTest))]
    public class CustomInspectorTestInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUIStyle bold_style = new GUIStyle()
            {
                fontSize = EditorStyles.boldFont.fontSize,
                fontStyle = FontStyle.Bold
            };
            EditorGUILayout.LabelField("CustomInspectorTest", bold_style);

            EditorGUI.indentLevel++;

            // for intValue
            var intValueProp = serializedObject.FindProperty("intValue");
            EditorGUILayout.PropertyField(intValueProp);

            // for stringValue
            var stringValueProp = serializedObject.FindProperty("stringValue");
            EditorGUILayout.PropertyField(stringValueProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif

}
