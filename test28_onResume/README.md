test28_onResume
====
UnityでAndroidのonResume(), onPause()を実装する例。

MonoBehaviour.OnApplicationPause()をオーバーライドする。

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // AndroidではonPause()時に呼び出される
        }
        else
        {
            // AndroidではonResume()時に呼び出される
            animator.SetTrigger("fade");
        }
    }

参考
  * [Unity - スクリプトリファレンス: MonoBehaviour.OnApplicationPause(bool)](http://docs.unity3d.com/ja/current/ScriptReference/MonoBehaviour.OnApplicationPause.html)