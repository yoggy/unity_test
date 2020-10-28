using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NPOI.XSSF.UserModel; // https://github.com/nissl-lab/npoi
using NPOI.SS.UserModel;

public class XSLXCommandContainer : ScriptableObject
{
    public List<XSLXCommand> command_list = new List<XSLXCommand>();

    public bool Append(IRow row)
    {
        var c = new XSLXCommand();

        c.command = row.GetCell(0)?.StringCellValue;
        c.str_0 = row.GetCell(1)?.StringCellValue;
        c.str_1 = row.GetCell(2)?.StringCellValue;
        c.str_2 = row.GetCell(3)?.StringCellValue;
        c.num_0 = SafeGetFloatCellValue(row, 4);
        c.num_1 = SafeGetFloatCellValue(row, 5);
        c.num_2 = SafeGetFloatCellValue(row, 6);
        c.bool_0 = SafeGetBoolCellValue(row, 7);
        c.text = row.GetCell(8)?.StringCellValue;
        c.comment = row.GetCell(9)?.StringCellValue;

        command_list.Add(c);

        return true;
    }

    float SafeGetFloatCellValue(IRow row, int idx)
    {
        var cell = row.GetCell(idx);
        if (cell == null) return 0.0f;

        return (float)cell.NumericCellValue;
    }

    bool SafeGetBoolCellValue(IRow row, int idx)
    {
        var cell = row.GetCell(idx);
        if (cell == null) return false;

        return cell.BooleanCellValue;
    }
}
