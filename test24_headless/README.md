test24_headless
====
Unityでビルドしたexeのオプションに-batchmodeを付けると、ヘッドレスモードで動作する。

    
    > test24_headless.exe -batchmode
    

マニュアルによると、-batchmodeは以下のように説明が書かれている。

   
   -batchmode    ゲームを “headless” モードで実行します。ゲームは何も表示せず、
                 ユーザー入力を受付しません。これはネットワークゲームでの
                 サーバー実行に最も便利です。
   

コマンドプロンプトからUnityでビルドしたexeを-batchmodeつきで起動すると、Windowsの場合、見えないウインドウを持つアプリが起動することになる。この場合、プログラムの終了を待たずにコマンドプロンプトを抜けてしまうため、コマンドプロンプトからCtrl+Cを入力してexeを止めることができない。

-batchmodeつきで起動したexeを終了する場合は、タスクマネージャからプログラムを終了する、もしくは、外部からの指示に応じてプログラムを終了する機能を実装しておく必要がある。

参考
----
* Unity - マニュアル: コマンドライン引数
  * http://docs.unity3d.com/ja/current/Manual/CommandLineArguments.html
  