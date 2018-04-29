using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class AspectHelper : MonoBehaviour {

    [SerializeField]
    Camera camera;

    [SerializeField]
    GameObject target_plane;

	void Update () {
        float x = target_plane.transform.localScale.x;
        float z = target_plane.transform.localScale.z;

        camera.aspect = x / z;
	}
}
