using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    public GameObject targetCamera;

	void Start () {
    }

    void Update () {
        UpdateBillboard();
    }

    void UpdateBillboard()
    {
        if (targetCamera == null) return;

        Vector3 p0 = gameObject.transform.position;
        Vector3 p1 = targetCamera.transform.position;

        // y軸回転のみ。x-z平面のみで向きを考える。
        p0.y = 0;
        p1.y = 0;

        var rot = Quaternion.FromToRotation(Vector3.back, p1 - p0);

        gameObject.transform.rotation = rot;
    }
}
