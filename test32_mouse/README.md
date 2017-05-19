test32_mouse
====
マウスカーソルを動かすと、GameObjectが追従するサンプル。

マウスカーソルの位置は、Input.mousePositionで取得することができる。

また、Input.mousePositionで取得できる位置はスクリーン座標系なので、
GameObjectの位置に反映する場合は、スクリーン座標系からワールド座標系に変換する必要がある。
座標系の変換は、Camera.main.ScreenToWorldPoint()を使用する。

	void Update () {
        Vector3 pos_screen = Input.mousePosition;
        Vector3 pos_world = Camera.main.ScreenToWorldPoint(pos_screen);
        pos_world.z = 100;
        
        gameObject.transform.position = pos_world;
    }
