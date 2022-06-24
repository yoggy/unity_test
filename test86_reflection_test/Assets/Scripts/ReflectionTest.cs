using System;
using System.Reflection;
using UnityEngine;

public class ReflectionTest : MonoBehaviour
{
    public TargetComponent targetComponent;

    void Awake()
    {
        // see also... https://qiita.com/satanabe1@github/items/08f7994d26840e14362d
        unsafe
        {
            MethodInfo methodOriginal = targetComponent.GetType().GetMethod("PressButtonMethod", BindingFlags.Public | BindingFlags.Instance);
            var ptr0 = methodOriginal.MethodHandle.Value.ToPointer();

            MethodInfo methodHook = this.GetType().GetMethod("HookPressButtonMethod", BindingFlags.NonPublic | BindingFlags.Instance);
            var ptr1 = methodHook.MethodHandle.Value.ToPointer();

            MethodInfo methodBackup = this.GetType().GetMethod("BackupPressButtonMethod", BindingFlags.NonPublic | BindingFlags.Instance);
            var ptr2 = methodBackup.MethodHandle.Value.ToPointer();

            // オリジナルのメソッドをBackupPressButtonMethod()へ退避
            *((int*)new IntPtr(((int*)ptr2 + 1)).ToPointer()) = *((int*)new IntPtr(((int*)ptr0 + 1)).ToPointer());

            // ターゲットのメソッドをHookメソッドへ差し替え
            *((int*)new IntPtr(((int*)ptr0 + 1)).ToPointer()) = *((int*)new IntPtr(((int*)ptr1 + 1)).ToPointer());
        }
    }

    void HookPressButtonMethod()
    {
        Debug.Log("ReflectionTest.HookPressButtonMethod()");

        // オリジナルを退避しているメソッドを呼び出す
        // 書き換え済みなので、targetComponent.PressButtonMethod(); はオリジナルのmethodを呼び出せないので注意
        this.BackupPressButtonMethod();
    }

    void BackupPressButtonMethod()
    {
        // オリジナルの関数ポインタで上書きされるので、ここは実行されない
    }
}
