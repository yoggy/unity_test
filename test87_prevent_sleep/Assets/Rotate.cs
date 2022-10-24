using UnityEngine;

public class Rotate : MonoBehaviour
{
    [Range(-1.0f, 1.0f)]
    public float rx = 0.1f;

    [Range(-1.0f, 1.0f)]
    public float ry = 0.2f;

    [Range(-1.0f, 1.0f)]
    public float rz = 0.3f;

    void Update()
    {
        transform.Rotate(rx, ry, rz);
    }
}
