using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

// https://docs.unity3d.com/ja/2019.4/Manual/RunningEditorCodeOnLaunch.html
[InitializeOnLoad]
public class EditorCoroutineInstaller
{
    // EditorCoroutine
    public static readonly string targetPackageName = "com.unity.editorcoroutines";

    static ListRequest _listRequest;
    static AddRequest _addRequest;

    // InitializeOnLoadを指定しておくと、エディタ起動時にここが呼び出される
    static EditorCoroutineInstaller()
    {
        ListRequest();
    }

    static void ListRequest()
    {
        // UnityEditor.PackageManager.Clientを使用して、UPMのパッケージ一覧を取得する
        // 少し時間がかかるので、EditorApplication.updateを使って処理完了まで待つ
        _listRequest = Client.List();
        EditorApplication.update += ListRequestUpdate;
    }

    static void ListRequestUpdate()
    {
        if (!_listRequest.IsCompleted) return;

        EditorApplication.update -= ListRequestUpdate;

        if (_listRequest.Status != StatusCode.Success)
        {
            //Debug.LogError($"listRequest error...listRequest.Status={_listRequest.Status}");
            return;
        }

        foreach (var packageInfo in _listRequest.Result)
        {
            //Debug.Log($"name={packageInfo.name}, displayName={packageInfo.displayName}");
            if (packageInfo.name == targetPackageName)
            {
                Debug.Log($"targetPackage is already installed...targetPackagename={targetPackageName}");
                return;
            }
        }

        AddPackage();
    }

    static void AddPackage()
    {
        // UnityEditor.PackageManager.Clientを使用して、UPMにパッケージを追加する
        // 少し時間がかかるので、EditorApplication.updateを使って処理完了まで待つ
        _addRequest = Client.Add(targetPackageName);
        EditorApplication.update += AddPackageUpdate;
    }

    static void AddPackageUpdate()
    {
        if (!_addRequest.IsCompleted) return;

        EditorApplication.update -= AddPackageUpdate;

        if (_addRequest.Status != StatusCode.Success)
        {
            Debug.LogError($"AddPackage failed...message={_addRequest.Error.message}");
            return;
        }

        Debug.Log($"AddPackage success!...targetPackageName={targetPackageName}");

        ImportUnityPakcageStage2();
    }

    static void ImportUnityPakcageStage2()
    {
        AssetDatabase.ImportPackage(Application.dataPath + "/Temp/Stage2.unitypackage", false);

        Debug.Log($"import Stage2.unitypackage");
    }

}
