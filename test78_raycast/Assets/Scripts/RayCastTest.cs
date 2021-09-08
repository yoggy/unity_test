using UnityEngine;

[ExecuteAlways]
public class RayCastTest : MonoBehaviour
{
    public GameObject objFrom;
    public GameObject objTo;

    public GameObject targetObj;
    public Texture2D targetTexture;

    void Update()
    {
        if (objFrom == null || objTo == null || targetObj == null) return;

        // for HitTest
        targetObj.layer = 8;

        var p_from = objFrom.transform.position;
        var p_to = objTo.transform.position;
        var direction = (p_to - p_from).normalized;

        // see also... https://docs.unity3d.com/ja/current/ScriptReference/Physics.Raycast.html
        RaycastHit hit;
        int layerMask = 1 << 8; // layer == 8 

        if (Physics.Raycast(p_from, direction, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(p_from, direction * hit.distance, Color.yellow);
            Debug.Log($"point={hit.point}, u={hit.textureCoord.x}, v={hit.textureCoord.y}");

            int x = Mathf.FloorToInt(hit.textureCoord.x * targetTexture.width);
            int y = Mathf.FloorToInt(hit.textureCoord.y * targetTexture.height);
            Color c = targetTexture.GetPixel(x, y);

            var matFrom = objFrom.GetComponent<Renderer>().material;
            matFrom.color = c;
        }
        else
        {
            Debug.DrawRay(p_from, direction * 1000, Color.white);
        }
    }
}
