using UnityEngine;
using UnityEditor;

public class CubeWireframeGenerator : MonoBehaviour {
    void Start()
    {
        MeshFilter mesh_filter = GetComponent<MeshFilter>();

        Mesh cube = mesh_filter.mesh;

        int[] idx = new int[] {
        0, 1,
        1, 3,
        3, 2,
        2, 0,
        4, 5,
        5, 7,
        7, 6,
        6, 4,
        0, 6,
        1, 7,
        2, 4,
        3, 5
    };

        mesh_filter.mesh.SetIndices(idx, MeshTopology.Lines, 0);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "save mesh"))
        {
            MeshFilter mesh_filter = GetComponent<MeshFilter>();
            if (mesh_filter.mesh != null)
            {
                AssetDatabase.CreateAsset(mesh_filter.mesh, "Assets/cube_wireframe.asset");
                AssetDatabase.SaveAssets();
            }
        }
    }
}
