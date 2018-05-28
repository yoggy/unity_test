using System.Collections.Generic;
using UnityEngine;

public class PointCloud : MonoBehaviour {

    void Start () 
    {
        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        float w = 1.0f;

        List<Vector3> v = new List<Vector3>();
        for (int i = 0; i < 10000; ++i) {
            float x = Random.Range(-w, w) * Random.Range(-w, w);
            float y = Random.Range(-w, w) * Random.Range(-w, w);
            float z = Random.Range(-w, w) * Random.Range(-w, w);
            v.Add(new Vector3(x, y, z));
        }
        mesh.SetVertices(v);

        int[] idxs = new int[mesh.vertices.Length];
        for (int i = 0; i < idxs.Length; i++)
        {
            idxs[i] = i;
        }
        mesh.SetIndices(idxs, MeshTopology.Points, 0);

        MeshFilter mesh_filter = gameObject.GetComponent<MeshFilter>();
        mesh_filter.sharedMesh = mesh;
    }
}
