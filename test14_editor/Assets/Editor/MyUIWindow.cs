using UnityEngine;
using UnityEditor;
using System.Collections;

public class MyUIWindow : EditorWindow
{
    string textFieldValue = "編集可能なテキスト";
    string textAreaValue = "複数行も\nかけるよ";

    bool button1Value;
    bool button2Value;

    Object obj;
    Material material;

    bool toggle1Value;
    bool toggle2Value;

    int intSliderValue;
    float sliderValue;

    float knobValue;

    [MenuItem("MyUI/MyUIWindow")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<MyUIWindow>("MyUIWindow");
    }

    void OnGUI()
    {
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.BeginVertical();

        GUILayout.Label("TextFieldの例");

        EditorGUI.indentLevel++;
        textFieldValue = EditorGUILayout.TextField(textFieldValue);
        textAreaValue = EditorGUILayout.TextArea(textAreaValue);
        EditorGUI.indentLevel--;

        GUILayout.Label("Buttonの例");
        EditorGUI.indentLevel++;
        using (new EditorGUILayout.HorizontalScope())
        {
            if (GUILayout.Button("Button1")) OnButton1();
            if (GUILayout.Button("Button2")) OnButton2();
        }
        EditorGUI.indentLevel--;

        GUILayout.Label("ObjectFieldの例");

        EditorGUI.indentLevel++;
        obj = EditorGUILayout.ObjectField(obj, typeof(Object), false);
        material = (Material)EditorGUILayout.ObjectField(material, typeof(Material), false);
        EditorGUI.indentLevel--;

        GUILayout.Label("Toggleの例");
        EditorGUI.indentLevel++;
        toggle1Value = EditorGUILayout.ToggleLeft("ToggleLeft", toggle1Value);
        toggle2Value = EditorGUILayout.Toggle("Toggle", toggle2Value);
        EditorGUI.indentLevel--;

        GUILayout.Label("Sliderの例");
        EditorGUI.indentLevel++;
        intSliderValue = EditorGUILayout.IntSlider(intSliderValue, 0, 50);
        sliderValue = EditorGUILayout.Slider(sliderValue, 0, 50);
        EditorGUI.indentLevel--;

        GUILayout.Label("Knobの例");
        knobValue = EditorGUILayout.Knob(new Vector2(50, 50), knobValue, 0, 100, "unit", Color.grey, Color.green, true);

        EditorGUILayout.EndVertical();

        // EditorGUI.BeginChangeCheck();が呼び出されてからGUIの値が変わっていたら、EditorGUI.EndChangeCheck()==trueになる
        if (EditorGUI.EndChangeCheck())
        {
            Debug.Log("toggle1Value=" + toggle1Value);
            Debug.Log("toggle2Value=" + toggle2Value);
            Debug.Log("intSliderValue=" + intSliderValue);
            Debug.Log("sliderValue=" + sliderValue);
        }
    }

    void OnButton1()
    {
        Debug.Log("Button1");
        string path = EditorUtility.OpenFilePanel("ファイルを選択してください", "", "json");

        if (path != "")
        {
            EditorUtility.DisplayDialog("情報", "ファイル" + path + "を選択しました", "閉じる");
        }
    }

    void OnButton2()
    {
        Debug.Log("Button2");
    }
}
