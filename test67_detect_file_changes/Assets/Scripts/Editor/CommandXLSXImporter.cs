using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;
using NPOI.XSSF.UserModel; // https://github.com/nissl-lab/npoi
using NPOI.SS.UserModel;

// see also... https://docs.unity3d.com/ja/current/ScriptReference/AssetPostprocessor.OnPostprocessAllAssets.html

public class CommandXLSXImporter : AssetPostprocessor
{
    static string target_xlsx_path = "Assets/commands.xlsx";
    static string target_assets_path = "Assets/commands.asset";

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string f in importedAssets)
        {
            if (f == target_xlsx_path)
            {
                ImportXSLX(target_xlsx_path);
            }
        }
    }

    [MenuItem("CommandXLSXImporter/ReimportXSLX")]
    static void ReimportXSLX()
    {
        ImportXSLX(target_xlsx_path);
    }

    static void ImportXSLX(string path)
    {
        Debug.Log("ImportXSLX : path=" + path);

        using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            IWorkbook book = new XSSFWorkbook(stream); // for *.xlsx
            ISheet sheet = book.GetSheetAt(0);
            if (sheet == null) return;

            // Scriptable Objectをロード
            XSLXCommandContainer commands = AssetDatabase.LoadAssetAtPath<XSLXCommandContainer>(target_assets_path);
            if (commands == null)
            {
                // 一番初めで.assetファイルがない場合は、新規に作成
                commands = ScriptableObject.CreateInstance<XSLXCommandContainer>();
                AssetDatabase.CreateAsset(commands, target_assets_path);
                AssetDatabase.ImportAsset(target_assets_path); // ←これを忘れると、しばらくProjectビューに表示されないので注意
            }

            // 一度内容をクリアしておく
            commands.command_list.Clear();

            int row_count = 0;
            while (true)
            {
                IRow row = sheet.GetRow(row_count);
                row_count++;

                if (row == null) continue;

                // コメント行は飛ばす
                ICell cel0 = row.GetCell(0);
                if (cel0 == null || cel0.CellType == CellType.Blank || cel0.StringCellValue == "") continue;
                if (cel0.StringCellValue.StartsWith("#")) continue;

                commands.Append(row);

                // command=endの場合は終了
                if (cel0.StringCellValue.StartsWith("end")) break;
            }

            EditorUtility.SetDirty(commands); // Scriptable Objectが更新されたことをセットする
            AssetDatabase.SaveAssets();       // Scriptable Objectを操作して更新した内容を.assetファイルへ反映する
            AssetDatabase.Refresh();
        }
    }
}
