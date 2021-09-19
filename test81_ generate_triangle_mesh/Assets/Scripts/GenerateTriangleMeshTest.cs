using UnityEngine;

[ExecuteAlways]
[DisallowMultipleComponent]
[RequireComponent(typeof(MeshFilter))]
public class GenerateTriangleMeshTest : MonoBehaviour
{
    public Transform p0;
    public Transform p1;
    public Transform p2;

    [Range(0, 5)]
    public int level = 2;

    void Update()
    {
        UpdateMesh();
    }

    void UpdateMesh()
    {
        if (p0 == null || p1 == null || p2 == null) return;

        var mesh = TriangleMesh.Generate(level, p0.position, p1.position, p2.position);
        var meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = mesh;        
    }
}
