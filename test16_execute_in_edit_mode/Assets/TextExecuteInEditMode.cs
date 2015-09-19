using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TextExecuteInEditMode : MonoBehaviour {
    [Header("Angular velocity Settings")]

    [ContextMenuItem("Reset", "ResetValues")] 
    [Range(-10.0f, 10.0f)]
    public float dx = 1.0f;

    [ContextMenuItem("Reset", "ResetValues")]
    [Range(-10.0f, 10.0f)]
    public float dy = 2.0f;

    [ContextMenuItem("Reset", "ResetValues")]
    [Range(-10.0f, 10.0f)]
    public float dz = 3.0f;

    void Start () {
	    
	}
	
	void Update () {
        gameObject.transform.Rotate(dx, dy, dz);	
	}

    void ResetValues()
    {
        dx = 1.0f;
        dy = 2.0f;
        dz = 3.0f;
    }
}
