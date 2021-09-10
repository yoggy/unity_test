using UnityEngine;

[ExecuteAlways]
public class TargetScript : MonoBehaviour
{
    void Update()
    {
#if TEST_DEFINE_SYMBOL
        Debug.Log("TargetScript.Update() : Enable TEST_DEFINE_SYMBOL");
#else
        Debug.Log("TargetScript.Update() : Disable TEST_DEFINE_SYMBOL");
#endif
    }
}
