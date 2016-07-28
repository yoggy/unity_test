using UnityEngine;
using System.Collections;

public class OnResumeScript : MonoBehaviour {

    [SerializeField]
    Animator animator;

	void Start () {
	
	}
	
	void Update () {
        // for debug...
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("fade");
        }
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // AndroidではonPause()時に呼び出される
        }
        else
        {
            // AndroidではonResume()時に呼び出される
            animator.SetTrigger("fade");
        }
    }
}
