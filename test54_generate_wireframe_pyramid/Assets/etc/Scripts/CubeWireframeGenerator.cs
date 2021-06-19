using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CubeWireframeGenerator : MonoBehaviour
{

    static CubeWireframeGenerator()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    static CubeWireframeGenerator GetInstance()
    {
        return (CubeWireframeGenerator)GameObject.FindObjectOfType(typeof(CubeWireframeGenerator));
    }

    static void OnSceneGUI(SceneView sceneView)
    {
        Handles.BeginGUI();
        if (GUI.Button(new Rect(10, 400, 160, 20), "Generate Cube Wireframe"))
        {
            GetInstance().GenerateCubeWireframe();
        }
        Handles.EndGUI();
    }

    public void GenerateCubeWireframe()
    {
        Debug.Log("GenerateCubeWireframe");

        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        List<Vector3> v = new List<Vector3>();
        v.Add(new Vector3(-0.5f, -0.5f, -0.5f));  // 0
        v.Add(new Vector3( 0.5f, -0.5f, -0.5f));  // 1
        v.Add(new Vector3( 0.5f,  0.5f, -0.5f));  // 2
        v.Add(new Vector3(-0.5f,  0.5f, -0.5f));  // 3
        v.Add(new Vector3(-0.5f, -0.5f,  0.5f));  // 4
        v.Add(new Vector3( 0.5f, -0.5f,  0.5f));  // 5
        v.Add(new Vector3( 0.5f,  0.5f,  0.5f));  // 6
        v.Add(new Vector3(-0.5f,  0.5f,  0.5f));  // 7
        mesh.SetVertices(v);

        List<int> idxs = new List<int>();
        idxs.Add(0); idxs.Add(1);
        idxs.Add(1); idxs.Add(2);
        idxs.Add(2); idxs.Add(3);
        idxs.Add(3); idxs.Add(0);
        idxs.Add(0); idxs.Add(4);
        idxs.Add(1); idxs.Add(5);
        idxs.Add(2); idxs.Add(6);
        idxs.Add(3); idxs.Add(7);
        idxs.Add(4); idxs.Add(5);
        idxs.Add(5); idxs.Add(6);
        idxs.Add(6); idxs.Add(7);
        idxs.Add(7); idxs.Add(4);

        mesh.SetIndices(idxs.ToArray(), MeshTopology.Lines, 0);

        MeshFilter mf = this.gameObject.GetComponent<MeshFilter>();
        mf.sharedMesh = mesh;

        MeshRenderer mr = this.gameObject.GetComponent<MeshRenderer>();
        if (mr.sharedMaterial == null)
        {
            mr.sharedMaterial = new Material(Shader.Find("Unlit/Color"));
        }
        mr.sharedMaterial.color = new Color(1.0f, 1.0f, 1.0f);

        // save the mesh to Assets directory
        AssetDatabase.CreateAsset(mesh, "Assets/wireframe_cube.asset");
        AssetDatabase.SaveAssets();
    }
}
