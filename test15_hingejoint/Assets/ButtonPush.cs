using UnityEngine;
using System.Collections;

public class ButtonPush : MonoBehaviour {

    public GameObject movingObject;

    bool enableMoving;
    float t;
    Vector3 originalPosition;

    void Start()
    {
        t = 0.0f;
        originalPosition = movingObject.transform.position;
    }

    void Update()
    {
        if (enableMoving == false)
        {
            t = 0.0f;
            movingObject.transform.position = originalPosition;
            return;
        }

        t += Time.deltaTime;
        movingObject.transform.Translate(0.0f, 0.1f, 0.0f);

        if (t > 1.0f)
        {
            enableMoving = false;
        }
    }

    public void OnClick()
    {
        enableMoving = true;
    }
}
