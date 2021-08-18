using UnityEngine;

public class InverseTransform : MonoBehaviour
{
    public Transform targetTransform;

    [ContextMenu("Test75InverseTransform")]
    public void Test75InverseTransform()
    {
        Debug.Log("Test75InverseTransform()");

        var target_pos = targetTransform.position;
        var target_rot = targetTransform.rotation;

        // see also... https://docs.unity3d.com/jp/current/ScriptReference/Matrix4x4.TRS.html
        var target_mat = Matrix4x4.TRS(target_pos, target_rot, new Vector3(1,1,1));

        // invertible matrix
        var inv_mat = target_mat.inverse;

        // see also... https://docs.unity3d.com/jp/current/ScriptReference/Matrix4x4.html
        var inv_pos = new Vector3(inv_mat[0, 3], inv_mat[1, 3], inv_mat[2, 3]);
        var inv_rot = inv_mat.rotation;

        gameObject.transform.position = inv_pos;
        gameObject.transform.rotation = inv_rot;
    }
}
