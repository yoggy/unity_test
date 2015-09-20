using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Connector : MonoBehaviour
{
    [Header("Connector Settings")]

    [ContextMenuItem("Reset", "ResetValues")]
    public GameObject fromGameObject;
    public GameObject toGameObject;

    void Update()
    {
        if (fromGameObject == null) return;
        if (toGameObject == null) return;

        Vector3 p0 = getCenter(fromGameObject);
        Vector3 p1 = getCenter(toGameObject);

        float scale_x = (p1 - p0).magnitude;

        var rot = Quaternion.FromToRotation(Vector3.right, p1 - p0);

        gameObject.transform.position = p0;
        gameObject.transform.localScale = new Vector3(scale_x, 1, 1);
        gameObject.transform.rotation = rot;
    }

    Vector3 getCenter(GameObject obj)
    {
        var boxCollider = obj.GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            return obj.transform.position + boxCollider.center;
        }

        var sphereCollider = obj.GetComponent<SphereCollider>();
        if (sphereCollider != null)
        {
            return obj.transform.position + sphereCollider.center;
        }

        var capsuleCollider = obj.GetComponent<CapsuleCollider>();
        if (capsuleCollider != null)
        {
            return obj.transform.position + capsuleCollider.center;
        }

        var meshCollider = obj.GetComponent<MeshCollider>();
        if (capsuleCollider != null)
        {
            return obj.transform.position + meshCollider.bounds.center;
        }

        var wheelCollider = obj.GetComponent<WheelCollider>();
        if (wheelCollider != null)
        {
            return obj.transform.position + wheelCollider.center;
        }

        var terrainCollider = obj.GetComponent<TerrainCollider>();
        if (terrainCollider != null)
        {
            return obj.transform.position + terrainCollider.bounds.center;
        }

        return new Vector3();
    }

    void ResetValues()
    {
        fromGameObject = null;
        toGameObject = null;
    }
}
