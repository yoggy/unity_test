using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rx = 0.1f;
    public float ry = 0.2f;
    public float rz = 0.3f;

    void Update()
    {
        transform.Rotate(rx, ry, rz);
    }
}
