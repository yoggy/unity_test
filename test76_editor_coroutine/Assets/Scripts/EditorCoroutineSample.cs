using System.Collections;
using UnityEngine;
using UnityEditor;
using Unity.EditorCoroutines.Editor; // https://docs.unity3d.com/Packages/com.unity.editorcoroutines@1.0/manual/index.html

public class EditorCoroutineSample : MonoBehaviour
{
    [MenuItem("test76_editor_coroutine/Test()")]
    private static void TestStatic()
    {
        EditorCoroutineSample module = FindObjectOfType<EditorCoroutineSample>();
        module.Test();
    }

    /////////////////////////////////////////////////////////////////////////////////

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
