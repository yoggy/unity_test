test25_barcordreader
====

UnityでUSBバーコードリーダの読み取り結果を受信するサンプル。

メモ
----

一般的に販売されているUSBバーコードリーダは、PCにUSB接続するとキーボードデバイスとして認識される製品が多い。

  - [バーコードリーダを検索した結果(amazon)](https://www.amazon.co.jp/s/?field-keywords=%E3%83%90%E3%83%BC%E3%82%B3%E3%83%BC%E3%83%89%E3%83%AA%E3%83%BC%E3%83%80%E3%83%BC)

これらのUSBバーコードリーダはバーコードを読み取ると、読み取った内容をキーの打鍵としてPC側へ出力する。
アプリ側はPCに入力されるキー入力を監視することでバーコードの読み取り結果を簡単に受信することができる。

UnityでUSBバーコードリーダの読み取り結果を受信する場合は[Input.inputString](https://docs.unity3d.com/ScriptReference/Input-inputString.html)を使用する。

USBバーコードリーダは読み取り結果をかなり短い時間内で出力するため、1フレーム内で複数のキー入力を受信することがある。
[Input.GetKeyDown](https://docs.unity3d.com/ScriptReference/Input.GetKeyDown.html)を使用した場合、打鍵の順番の判別ができなくなる可能性があるので、1フレーム内ででキーボードで入力された文字を返す[Input.inputString](https://docs.unity3d.com/ScriptReference/Input-inputString.html)を使用するのがおすすめ。

