# test54_generate_wireframe_pyramid
![img01.png](img01.png)

    public void GeneratePyramidWireframe()
    {
        Debug.Log("GeneratePyramidWireframe");

        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        List<Vector3> v = new List<Vector3>();
        v.Add(new Vector3(   0,      0, 0));  // 0
        v.Add(new Vector3(-0.5f, -0.5f, 1));  // 1
        v.Add(new Vector3( 0.5f, -0.5f, 1));  // 2
        v.Add(new Vector3( 0.5f,  0.5f, 1));  // 3
        v.Add(new Vector3(-0.5f,  0.5f, 1));  // 4
        mesh.SetVertices(v);

        List<int> idxs = new List<int>();
        idxs.Add(0); idxs.Add(1);
        idxs.Add(0); idxs.Add(2);
        idxs.Add(0); idxs.Add(3);
        idxs.Add(0); idxs.Add(4);
        idxs.Add(1); idxs.Add(2);
        idxs.Add(2); idxs.Add(3);
        idxs.Add(3); idxs.Add(4);
        idxs.Add(4); idxs.Add(1);

        mesh.SetIndices(idxs.ToArray(), MeshTopology.Lines, 0);

        MeshFilter mf = this.gameObject.GetComponent<MeshFilter>();
        mf.sharedMesh = mesh;

        MeshRenderer mr = this.gameObject.GetComponent<MeshRenderer>();
        mr.sharedMaterial.color = new Color(1.0f, 1.0f, 1.0f);

        // save the mesh to Assets directory
        AssetDatabase.CreateAsset(mesh, "Assets/pyramid_wireframe.asset");
        AssetDatabase.SaveAssets();
    }
