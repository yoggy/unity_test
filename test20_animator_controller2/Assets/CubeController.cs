using UnityEngine;
using System.Collections;

public class CubeController : MonoBehaviour {

    public Animator animator;
    public SphereController sphere;

    bool _fire = false;

    public void Fire()
    {
        _fire = true;
    }

    void Start () {
	
	}
	
	void Update () {
        if (_fire)
        {
            _fire = false;
            if (animator != null) animator.SetTrigger("fire");
            if (sphere != null) sphere.Fire();
        }
    }
}
