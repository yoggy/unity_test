using UnityEngine;
using System.Collections;

public class ButtonClickSphere : MonoBehaviour {
    public SphereController sphere;

    public void OnClick()
    {
        if (sphere != null) sphere.Fire();
    }
}
