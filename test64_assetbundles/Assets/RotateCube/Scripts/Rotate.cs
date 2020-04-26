using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float pitch;
    public float yaw;
    public float roll;

    void Update()
    {
        float rx = pitch * Time.deltaTime;
        float ry = yaw * Time.deltaTime;
        float rz = roll * Time.deltaTime;

        transform.Rotate(rx, ry, rz);
    }
}
