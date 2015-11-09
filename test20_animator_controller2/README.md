memo
====

構成
----

    Clickまたはボタン → CubeController.Fire()   → Cube1のAnimatorAnimator.SetTrigger("fire")
                          ↓
                         SphereController.Fire() → SphereのAnimator.SetTrigger("fire")
                          ↓(1秒後)
                         CubeController.Fire()   → Cube2のAnimatorAnimator.SetTrigger("fire")


clickイベントの取り方
----
1. シーンにEventSystemを追加する
  - GameObject → UI → Event System

2. メインカメラにPhysics Raycasterをアタッチする。

3. IPointerClickHandlerを実装したクラスを作成。GameObjectにアタッチする。

    public class Click : MonoBehaviour, IPointerClickHandler
    {
        public CubeAnimation cube;
    
        public void OnPointerClick(PointerEventData eventData)
        {
            if (cube == null) return;
            cube.Fire();
        }
    }

