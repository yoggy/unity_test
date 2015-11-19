音のメモ
====
- Unityでは3D空間内でAudioSourceが音を出して、AudioListenerが音を聞く役割
- デフォルトではAudioListenerはカメラに追加されている。
- 音が出るオブジェクトを作成する場合は…
  - メニューのGameObject→Audio→Audio SourceでAudio Source付きのGameObjectが作成される
  - または、GameObjectにAudio SourceをAdd Componentで追加する

Audio Sourceのメモ
---
- Play On Awakeにチェックを入れると、起動時に再生される
- Loopをチェック入れておくと、ループ再生
- AudioSource.Play()で再生
- 音を混ぜすぎると音が割れるみたいなので、AudioSource.Volumeを0.6ぐらいに設定。