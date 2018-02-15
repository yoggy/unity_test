using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshLine : MonoBehaviour {
	void Start () {
        var mesh = new Mesh();
        mesh.vertices = new Vector3[] {
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3( 0.5f, -0.5f, -0.5f),
            new Vector3( 0.5f,  0.5f, -0.5f),
            new Vector3(-0.5f,  0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f,  0.5f,  0.5f),
            new Vector3(-0.5f,  0.5f,  0.5f),
        };

        int[] idxs = new int[] {
            0, 1,
            1, 2,
            2, 3,
            3, 0,
            0, 4,
            1, 5,
            2, 6,
            3, 7,
            4, 5,
            5, 6,
            6, 7,
            7, 4,
        };

        mesh.SetIndices(idxs, MeshTopology.Lines, 0);

        MeshFilter mesh_filter = GetComponent<MeshFilter>();
        mesh_filter.sharedMesh = mesh;
    }
}
