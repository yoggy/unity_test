Animator Controllerのメモ

参考
========
アニメーターコントローラー
  * http://docs.unity3d.com/ja/current/Manual/class-AnimatorController.html

アニメーションパラメーター
  * http://docs.unity3d.com/ja/current/Manual/AnimationParameters.html

手順
========
* アニメーションamin1, amim2を作成
* アニメーションコントローラ(Cube)を開く
  * ParameterにTriggerを追加
    * "anim1", "anim2"
  * ステートを設定
    * "animation1", "animation2"
  * ステートの遷移(AnimationTransitionBase)を設定
    * ステートを選択→右クリックメニューから"Make Transition"→遷移先を選択
    * 遷移を表す矢印線をクリック。inspectorにAnimationTransitionBaseのプロパティが表示される。
    * 次の遷移条件(Conditions)を設定
      * animation1→animation2の条件はトリガ"anim2"発生時
      * animation2→animation1の条件はトリガ"anim1"発生時
    * 遷移条件を何も指定しないと、アニメーションが終わったら、
      勝手に次のアニメーションに遷移してしまう。

* ボタンの設定
  * ButtonのOnClick()に次の設定を追加
    * ButtonAnim1のボタン
      * Scene中"Cube"のAnimator.SetTrigger(String)に"anim1"
      * Scene中"ButtonAnim1"のButton.intaractableをfalse
      * Scene中"ButtonAnim2"のButton.intaractableをtrue
    * ButtonAnim2のボタン
      * Scene中"Cube"のAnimator.SetTrigger(String)に"anim2"
      * Scene中"ButtonAnim1"のButton.intaractableをtrue
      * Scene中"ButtonAnim2"のButton.intaractableをfalse

メモ
========
アニメーションコントローラのパラメータTriggerは、遷移が完了した際に
フラグがクリアされる。
Button.intaractableを設定しているのは、アニメーションanim1が有効な間に
SetTriger("anim1")すると、遷移が完了していないので、フラグが消えないため。

アニメーションの遷移(AnimationTransitionBase)には、
anim1→anim2へ遷移する際のブレンド具合を指定することが可能。
遷移を表す矢印を選択すると、Inspectorにタイムラインが表示されている。
