using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RaycastTest : MonoBehaviour
{
    public GameObject objFrom;
    public GameObject objTo;

    public GameObject targetObject;

    void Update()
    {
        if (targetObject == null) return;

        int layerMask = 1 << 8;  // layer = 8

        RaycastHit hit;

        Vector3 direction = objTo.transform.position - objFrom.transform.position;

        if (Physics.Raycast(objFrom.transform.position, direction, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(objFrom.transform.position, direction * hit.distance, Color.yellow);
            //Debug.Log($"Hit u={hit.textureCoord.x}, v={hit.textureCoord.y}");
        }
        else
        {
            Debug.DrawRay(objFrom.transform.position, objFrom.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }
}
