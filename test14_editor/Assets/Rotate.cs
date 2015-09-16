using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    [Header("回転角速度の設定")]

    [ContextMenuItem("Reset", "ResetValues")] // コンテキストメニューを追加。第一引数はメニュー文字列。第二引数は実行する関数
    [Range(-10.0f, 10.0f)]                    // Rangeを指定すると、GUIがスライダーに変化する
    public float dx = 1.0f;

    [ContextMenuItem("Reset", "ResetValues")]
    [Range(-10.0f, 10.0f)]
    public float dy = 2.0f;

    [ContextMenuItem("Reset", "ResetValues")]
    [Range(-10.0f, 10.0f)]
    public float dz = 3.0f;

    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(dx, dy, dz);
    }

    void ResetValues()
    {
        dx = 1.0f;
        dy = 2.0f;
        dz = 3.0f;
    }
}