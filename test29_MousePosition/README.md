test29_MousePosition
====
マウスカーソルの位置にgameObjectを移動させるサンプル。

スクリーン座標系→ワールド座標の変換方法
----
マウスカーソルの位置はInput.mousePositionで取得可能。

  * [Unity - スクリプトリファレンス: Input.mousePosition](http://docs.unity3d.com/jp/current/ScriptReference/Input-mousePosition.html)

取得できる値はスクリーン座標。画面左下(0,0)～画面右上(Screen.width, Screen.height)の範囲の値。

スクリーン座標は Camera.main.ScreenToWorldPoint()を使ってワールド座標に変換できる。

* [Unity - スクリプトリファレンス: Camera.ScreenToWorldPoint](http://docs.unity3d.com/jp/current/ScriptReference/Camera.ScreenToWorldPoint.html)

sample code
----
マウス左ボタンをドラッグすると、gameObjectの位置をマウスカーソルの位置に動かすサンプル。

    using UnityEngine;
    
    public class MousePosition : MonoBehaviour {
    
        void Start () {	
    	}
    	
    	void Update () {
            if (Input.GetMouseButton(0))
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
    
                Vector3 pos_screen2d = Input.mousePosition;
                pos_screen2d.z = 1f;
                Vector3 pos_world3d = Camera.main.ScreenToWorldPoint(pos_screen2d);
                gameObject.transform.position = pos_world3d;
            }
            else
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
