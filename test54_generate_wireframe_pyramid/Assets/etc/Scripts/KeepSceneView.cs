// see also...https://github.com/yoggy/unity_test/tree/master/test40_keep_scene_view
using UnityEngine;

public class KeepSceneView : MonoBehaviour
{
    void Start()
    {
        if (Application.isEditor)
        {
            UnityEditor.SceneView.FocusWindowIfItsOpen(typeof(UnityEditor.SceneView));
        }
    }
}