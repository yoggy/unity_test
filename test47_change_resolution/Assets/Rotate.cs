using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField]
    float rx;

    [SerializeField]
    float ry;

    [SerializeField]
    float rz;

    void Start()
    {
    }

    void Update()
    {
        gameObject.transform.Rotate(rx, ry, rz);
    }
}