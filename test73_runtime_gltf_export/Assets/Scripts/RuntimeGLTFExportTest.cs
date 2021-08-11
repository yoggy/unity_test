using System.IO;
using UnityEngine;
using UniGLTF;
using VRMShaders;

public class RuntimeGLTFExportTest : MonoBehaviour
{
    public GameObject targetObject;

    public void Export()
    {
        var settings = new GltfExportSettings();

        var gltf = new glTF();
        using (var exporter = new gltfExporter(gltf, settings)) 
        {
            exporter.Prepare(targetObject);
            exporter.Export(settings, new RuntimeTextureSerializer());
        }

        var path = Path.Combine(Application.streamingAssetsPath, "test.gltf");

        var bytes = gltf.ToGlbBytes();
        File.WriteAllBytes(path, bytes);
    }    
}
