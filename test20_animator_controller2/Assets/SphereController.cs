using UnityEngine;
using System.Collections;

public class SphereController : MonoBehaviour
{

    public Animator sphereAnimator;
    public CubeController cubeController;

    bool _fire = false;
    public float _t = 0.0f;

    public void Fire()
    {
        _fire = true;
    }

    void Start()
    {

    }

    void Update()
    {
        if (_fire)
        {
            _fire = false;
            _t = 1.0f;

            if (sphereAnimator != null)
            {
                sphereAnimator.SetTrigger("fire");
            }
        }

        if (_t > 0.0f)
        {
            _t -= Time.deltaTime;
            if (_t <= 0.0f)
            {
                if (cubeController != null)
                {
                    cubeController.Fire();
                }
                _t = 0.0f;
            }
        }
    }
}
