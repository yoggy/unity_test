using UnityEngine;

public class Line : MonoBehaviour {

    [SerializeField]
    GameObject fromGameObject;

    [SerializeField]
    GameObject toGameObject;

    LineRenderer lineRenderer;

    void Start () {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }
    
    void Update () {
        if (lineRenderer == null) return;
        if (fromGameObject == null) return;
        if (toGameObject == null) return;

        lineRenderer.SetPosition(0, fromGameObject.transform.position);
        lineRenderer.SetPosition(1, toGameObject.transform.position);
    }
}
