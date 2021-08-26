using System.Collections;
using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor; // https://docs.unity3d.com/Packages/com.unity.editorcoroutines@1.0/manual/index.html

public class EditorCoroutineSample : MonoBehaviour
{
    public IEnumerator TestCoroutine()
    {
        for (int i = 0; i < 10; ++i)
        {
            yield return new EditorWaitForSeconds(1.0f);
            Debug.Log($"count={i}");
        }
    }

    [ContextMenu("EditorCoroutineSample.Test()")]
    public void Test()
    {
        EditorCoroutineUtility.StartCoroutineOwnerless(TestCoroutine());

        // EditorCoroutineUtility.StartCoroutine(TestCoroutine(), this );
    }
}