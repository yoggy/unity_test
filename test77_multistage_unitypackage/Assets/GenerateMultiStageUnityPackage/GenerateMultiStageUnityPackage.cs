using UnityEngine;
using UnityEditor;

// 
// unitypackageの中にStage2.unitypackageを格納しておいて、unitypackageインポート完了時にStage2.unitypackageを自動インポートする例
//
// この例では、Stage1でcom.unity.editorcoroutinesのインストール、Stage2でEditorCoroutineを使ったサンプコードをインポートする。
//
// 動作は次の通り
//     - EditorCoroutineInstaller.unitypackage をインポートする
//         - EditorCoroutineInstaller/Scripts/Editor/EditorCoroutineInstaller.cs
//         - Temp/Stage2.unitypackage
//     - unitypackageインポート完了時にInitializeOnLoadが付加されているEditorCoroutineInstallerクラスが実行される
//         - UPMでcom.unity.editorcoroutinesをインストール
//         - Stage2.unitypackageのインポートを実行
//             - Stage2.unitypackage にはEditorCoroutineを使ったサンプコードが格納されている
//
// EditorCoroutineを使ったサンプルコードをEditorCoroutineInstaller.unitypackage側に含めてしまうと、
// インポート完了時にはまだcom.unity.editorcoroutinesがインストールされていない状態なので、
// コンパイルエラーでスクリプトが停止してしまう。
// InitializeOnLoadが実行されないため、unitypackageをインポートしても、com.unity.editorcoroutinesパッケージがインストールされず、
// EditorCoroutineを使ったサンプコードの参照が足りずにエラー状態になる…
// 
public class GenerateMultiStageUnityPackage : MonoBehaviour
{
    static string[] assetsPath2nd = new string[]
    {
        "Assets/SampleScripts",
    };
    static string unityPackagePath2nd = "Assets/Temp/Stage2.unitypackage";

    static string[] assetsPath1st = new string[]
    {
        "Assets/EditorCoroutineInstaller",
        "Assets/Temp/Stage2.unitypackage"
    };
    static string unityPackagePath1st = "Assets/Temp/EditorCoroutineInstaller.unitypackage";

    [MenuItem("test77_editor_coroutine/Generate Multi-Stage UnityPackage")]
    static void GenerateUnityPackages()
    {
        // generate Stage2 (Stage2.unitypackage)
        GenerateUnityPackage(assetsPath2nd, unityPackagePath2nd, false);

        // generate Stage1 (EditorCoroutineInstaller.unitypackage)
        GenerateUnityPackage(assetsPath1st, unityPackagePath1st, true);

        Debug.Log("GenerateMultiStageUnityPackage.Generate() : success!");
    }

    static void GenerateUnityPackage(string[] assetsPath, string unityPackagePath, bool isInteractive)
    {
        var options = ExportPackageOptions.IncludeDependencies | ExportPackageOptions.Recurse;
        if (isInteractive == true)
        {
            options |= ExportPackageOptions.Interactive; // untiypackageができたらフォルダを開く
        }

        AssetDatabase.ExportPackage(assetsPath, unityPackagePath, options);
    }
}
