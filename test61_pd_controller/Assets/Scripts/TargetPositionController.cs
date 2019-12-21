using UnityEngine;
using UnityEditor;

public class TargetPositionController : MonoBehaviour
{
    void OnSceneGUI(SceneView sceneView)
    {
        Handles.BeginGUI();
        GUILayout.Window(1, new Rect(3, 20, 200, 50), OnWindowGUI, "PD Controller test");
        Handles.EndGUI();
    }

    void OnWindowGUI(int id)
    {
        if (GUILayout.Button("move"))
        {
            if (gameObject.transform.position.x == 0) {
                gameObject.transform.position = new Vector3(1, 0, 0);
            }
            else {
                gameObject.transform.position = Vector3.zero;
            }
        }
    }
    void Start()
    {
        SceneView.duringSceneGui += this.OnSceneGUI;
    }

    void OnDestroy()
    {
        SceneView.duringSceneGui -= this.OnSceneGUI;
    }
}
