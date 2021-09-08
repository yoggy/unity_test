using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MeshTextureTest : MonoBehaviour
{
    public GameObject targetObject;

    public MeshFilter triangleMeshFilter;

    public Transform from;
    public Transform p0Transform;
    public Transform p1Transform;
    public Transform p2Transform;

    void Update()
    {
        Vector3 p0 = p0Transform.position;
        Vector3 p1 = p1Transform.position;
        Vector3 p2 = p2Transform.position;

        Vector3[] v = new Vector3[45];
        v[0] = p0;
        v[36] = p1;
        v[44] = p2;

        // lv1
        v[10] = (v[0] + v[36]) / 2;
        v[14] = (v[0] + v[44]) / 2;
        v[40] = (v[36] + v[44]) / 2;

        // lv2
        v[3] = (v[0] + v[10]) / 2;
        v[5] = (v[0] + v[14]) / 2;
        v[12] = (v[10] + v[14]) / 2;

        v[21] = (v[10] + v[36]) / 2;
        v[23] = (v[10] + v[40]) / 2;
        v[38] = (v[36] + v[40]) / 2;

        v[25] = (v[14] + v[40]) / 2;
        v[27] = (v[14] + v[44]) / 2;
        v[42] = (v[40] + v[44]) / 2;

        // lv3
        v[1] = (v[0] + v[3]) / 2;
        v[2] = (v[0] + v[5]) / 2;
        v[4] = (v[3] + v[5]) / 2;

        v[6] = (v[3] + v[10]) / 2;
        v[7] = (v[3] + v[12]) / 2;
        v[11] = (v[10] + v[12]) / 2;

        v[8] = (v[5] + v[12]) / 2;
        v[9] = (v[5] + v[14]) / 2;
        v[13] = (v[12] + v[14]) / 2;

        v[15] = (v[10] + v[21]) / 2;
        v[16] = (v[10] + v[23]) / 2;
        v[22] = (v[21] + v[23]) / 2;

        v[17] = (v[12] + v[23]) / 2;
        v[18] = (v[12] + v[25]) / 2;
        v[24] = (v[23] + v[25]) / 2;

        v[19] = (v[14] + v[25]) / 2;
        v[20] = (v[14] + v[27]) / 2;
        v[26] = (v[25] + v[27]) / 2;

        v[28] = (v[21] + v[36]) / 2;
        v[29] = (v[21] + v[38]) / 2;
        v[37] = (v[36] + v[38]) / 2;

        v[30] = (v[23] + v[38]) / 2;
        v[31] = (v[23] + v[40]) / 2;
        v[39] = (v[38] + v[40]) / 2;

        v[32] = (v[25] + v[40]) / 2;
        v[33] = (v[25] + v[42]) / 2;
        v[41] = (v[40] + v[42]) / 2;

        v[34] = (v[27] + v[42]) / 2;
        v[35] = (v[27] + v[44]) / 2;
        v[43] = (v[42] + v[44]) / 2;

        RaycastHit[] hits = new RaycastHit[v.Length];
        for (int i = 0; i < hits.Length; ++i)
        {
            if (HitTest(from, v[i], out hits[i]) == false)
            {
                return;
            }
        }

        Debug.Log($"hit!");

        Mesh mesh = new Mesh();
        mesh.SetVertices(v);

        var uvs = new List<Vector2>();
        foreach (var h in hits)
        {
            uvs.Add(h.textureCoord);
        }
        mesh.uv = uvs.ToArray();

        int[] idx = new int[] {
             0, 1, 2,
             1, 3, 4,
             2, 1, 4,
             2, 4, 5,
             3, 6, 7,
             4, 3, 7,
             4, 7, 8,
             5, 4, 8,
             5, 8, 9,
             6, 10, 11,
             7, 6, 11,
             7, 11, 12,
             8, 7, 12,
             8, 12, 13,
             9, 8, 13,
             9, 13, 14,
             10,15,16,
             11,10,16,
             11,16,17,
             12,11,17,
             12,17,18,
             13,12,18,
             13,18,19,
             14,13,19,
             14,19,20,
             15,21,22,
             16,15,22,
             16,22,23,
             17,16,23,
             17,23,24,
             18,17,24,
             18,24,25,
             19,18,25,
             19,25,26,
             20,19,26,
             20,26,27,
             21,28,29,
             22,21,29,
             22,29,30,
             23,22,30,
             23,30,31,
             24,23,31,
             24,31,32,
             25,24,32,
             25,32,33,
             26,25,33,
             26,33,34,
             27,26,34,
             27,34,35,
             28,36,37,
             29,28,37,
             29,37,38,
             30,29,38,
             30,38,39,
             31,30,39,
             31,39,40,
             32,31,40,
             32,40,41,
             33,32,41,
             33,41,42,
             34,33,42,
             34,42,43,
             35,34,43,
             35,43,44,
        };
        mesh.SetIndices(idx, MeshTopology.Triangles, 0);

        mesh.RecalculateNormals();

        triangleMeshFilter.sharedMesh = mesh;
    }

    bool HitTest(Transform from, Vector3 to, out RaycastHit hit)
    {
        int layerMask = 1 << 8;  // layer = 8

        Vector3 direction = to - from.position;

        return Physics.Raycast(from.position, direction, out hit, Mathf.Infinity, layerMask);
    }
}
