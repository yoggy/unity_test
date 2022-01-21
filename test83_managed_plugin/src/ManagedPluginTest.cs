using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ManagedPluginTestWindow : EditorWindow
{
    public static ManagedPluginTestWindow _window;

    [MenuItem("Managed Plugin Test/Show Test Window")]
    private static void OpenWindow()
    {
        WindowInit();
        _window.Show();
    }

    public static void WindowInit()
    {
        if (_window == null)
        {
            _window = GetWindow<ManagedPluginTestWindow>("ManagedPluginTest");
            _window.minSize = new Vector2(640, 480);
        }
    }

    void OnEnable()
    {
        Debug.Log("OnEnable");
        WindowInit();
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy");
    }

    Vector2 _scrollPosition;

    void OnGUI()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("ManagedPluginTest");
        EditorGUILayout.EndVertical();

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

        for (int i = 0; i < 10; ++i)
        {
            GUILayout.Box(String.Format("## Title = {0} ##", i), GUILayout.ExpandWidth(true), GUILayout.Height(20));

            EditorGUILayout.TextField("Sample Text", "sample text");

            if (GUILayout.Button(String.Format("Sample Button {0}", i), GUILayout.Width(240)))
            {
                Debug.Log(String.Format("Sample Button {0}", i));
            }

            GUILayout.Space(20);
        }

        EditorGUILayout.EndScrollView();
    }

}