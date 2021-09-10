using UnityEngine;
using UnityEditor;

public class ModifyScriptingDefineSymbolsTest : MonoBehaviour
{
    [MenuItem("test80_modify_scripting_define_symbols/Enable %e", priority = 2)]
    static void ModifyScriptingDefineSymbolEnable()
    {
        // シンボルを複数定義する場合は、;区切りの文字列で設定
        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, "TEST_DEFINE_SYMBOL");

        // シンボルを複数定義している場合は、;区切りの文字列で取得できる
        var defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

        Debug.Log($"ModiryScriptingDefineSymbolEnable() : defineSymbols={defineSymbols}");
    }

    [MenuItem("test80_modify_scripting_define_symbols/Disable %d", priority = 1)]
    static void ModifyScriptingDefineSymbolDisable()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, "");

        var defineSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);

        Debug.Log($"ModifyScriptingDefineSymbolDisable() : defineSymbols={defineSymbols}");
    }
}
