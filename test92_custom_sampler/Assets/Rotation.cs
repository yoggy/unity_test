using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rx = 0.05f;
    public float ry = 0.1f;
    public float rz = 0.2f;

    void Start()
    {
        transform.Rotate(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
    }

    void Update()
    {
        transform.Rotate(rx, ry, rz);
    }
}
