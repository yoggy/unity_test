using UnityEngine;

[ExecuteAlways]
[DisallowMultipleComponent]
public class Caliper : MonoBehaviour
{
    public Transform p0;
    public Transform p1;
    public TextMesh text;
    public LineRenderer line;

    void Update()
    {
        Vector3[] ps = new Vector3[2];
        ps[0] = p0.position;
        ps[1] = p1.position;
        line.SetPositions(ps);

        Vector3 center = Vector3.Lerp(p0.position, ps[1] = p1.position, 0.5f);
        text.transform.position = center;

        float d = Vector3.Distance(p0.position, p1.position);
        text.text = $"{d:F3}m";

#if UNITY_EDITOR
        Vector3 p = UnityEditor.SceneView.lastActiveSceneView.camera.transform.position;
        text.transform.LookAt(p, Vector3.up);
#endif
    }
}
