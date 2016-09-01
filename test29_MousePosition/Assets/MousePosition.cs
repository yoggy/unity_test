using UnityEngine;

public class MousePosition : MonoBehaviour
{

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;

            Vector3 pos_screen2d = Input.mousePosition;
            pos_screen2d.z = 1f;
            Vector3 pos_world3d = Camera.main.ScreenToWorldPoint(pos_screen2d);
            gameObject.transform.position = pos_world3d;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
