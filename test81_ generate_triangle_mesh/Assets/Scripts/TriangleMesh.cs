// https://github.com/yoggy/unity_test/test81_ generate_triangle_mesh/Assets/Scripts/TriangleMesh.cs
using System.Collections.Generic;
using UnityEngine;

public class Triangle
{
    public TriangleMesh container;

    public int idx0;
    public int idx1;
    public int idx2;

    public Triangle(TriangleMesh container, int idx0, int idx1, int idx2)
    {
        this.container = container;

        this.idx0 = idx0;
        this.idx1 = idx1;
        this.idx2 = idx2;
    }

    public int AddVertex(Vector3 p) {
        return container.AddVertex(p);
    }

    public Vector3 GetVertex(int idx)
    {
        return container.GetVertex(idx);
    }

    //
    //  p1 - p2     p1 - p4 - p2
    //  |  /    ->  |  / |  /
    //  p0          p3 - p5
    //              |  /
    //              p0
    //
    //  p0 - p3 - p5
    //  p3 - p1 - p4
    //  p5 - p3 - p4
    //  p5 - p4 - p2
    // 
    public List<Triangle> Divide4()
    {
        var p0 = GetVertex(idx0);
        var p1 = GetVertex(idx1);
        var p2 = GetVertex(idx2);

        var p3 = (p0 + p1) / 2;
        var p4 = (p1 + p2) / 2;
        var p5 = (p2 + p0) / 2;

        var idx3 = AddVertex(p3);
        var idx4 = AddVertex(p4);
        var idx5 = AddVertex(p5);

        var rv = new List<Triangle>();
        rv.Add(new Triangle(container, idx0, idx3, idx5));
        rv.Add(new Triangle(container, idx3, idx1, idx4));
        rv.Add(new Triangle(container, idx5, idx3, idx4));
        rv.Add(new Triangle(container, idx5, idx4, idx2));

        return rv;
    }
}

public class TriangleMesh
{
    public List<Vector3> vetrices = new List<Vector3>();
    public List<int> indices = new List<int>();

    public static Mesh Generate(int level, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        Mesh mesh = new Mesh();

        TriangleMesh triangle_mesh = new TriangleMesh(level, p0, p1, p2);

        mesh.SetVertices(triangle_mesh.vetrices);
        mesh.SetIndices(triangle_mesh.indices, MeshTopology.Triangles, 0);
        mesh.RecalculateNormals();

        return mesh;
    }

    public int AddVertex(Vector3 p)
    {
        for (int i = 0; i < vetrices.Count; ++i) {
            var v = vetrices[i];
            if (Vector3.Distance(p, v) <= 1e-5) { // see also... https://docs.unity3d.com/ja/current/ScriptReference/Vector3-operator_eq.html
                return i;
            }
        }

        int idx = vetrices.Count;
        vetrices.Add(p);
        return idx;
    }

    public Vector3 GetVertex(int idx)
    {
        return vetrices[idx];
    }

    public TriangleMesh(int level, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        vetrices.Clear();
        indices.Clear();

        int idx0 = AddVertex(p0);
        int idx1 = AddVertex(p1);
        int idx2 = AddVertex(p2);

        var current = new Queue<Triangle>();
        current.Enqueue(new Triangle(this, idx0, idx1, idx2));

        var next = new Queue<Triangle>();

        for (int i = 0; i < level; ++i) {
            next.Clear();
            while(current.Count > 0) {
                var target_t = current.Dequeue();
                var divide_t4 = target_t.Divide4();
                foreach(var t in divide_t4) {
                    next.Enqueue(t);
                }
            }
            current = new Queue<Triangle>(next);
        }

        while(current.Count > 0) {
            var t = current.Dequeue();
            indices.Add(t.idx0);
            indices.Add(t.idx1);
            indices.Add(t.idx2);
        }
    }

    public override string ToString()
    {
        string str = "";

        str += "{\"indices\":[";
        for (int i = 0; i < indices.Count; i += 3) {
            var i0 = indices[i];
            var i1 = indices[i+1];
            var i2 = indices[i+2];
            str += $"[{i0},{i1},{i2}],";
        }
        str += "],\n";
        str += "\"vertices\":[";
        for (int i = 0; i < vetrices.Count; i += 3) {
            var v0 = vetrices[i];
            var v1 = vetrices[i+1];
            var v2 = vetrices[i+2];
            str += $"[{v0},{v1},{v2}],";
        }
        str += "]}";

        return str;
    }
}
