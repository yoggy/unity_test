using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepSceneView : MonoBehaviour
{
    void Start()
    {
        // see also... https://stackoverflow.com/questions/9337700/in-unity3d-is-it-possible-to-keep-the-scene-view-focused-when-hitting-play
        if (Application.isEditor)
        {
            UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
        }
    }
}