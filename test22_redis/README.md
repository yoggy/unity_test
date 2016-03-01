test22_redis - UnityからRedisに接続するサンプル
====

このサンプルを動かす場合は、localhostでRedisが実行されている必要があります。

サンプルでは以下のC#のRedisクライアントライブラリを使用しています。

  * [TeamDev C# Redis Client](https://redis.codeplex.com/)

実行中にエディタ上で左の球を上下に動かすと、それに追従して右の立方体が動きます。
左のSphereは位置をRedisに書き込み続ける動作を行っています。(SetPositionToRedis.cs)

右の立方体はRedisからその情報を読み込み、位置へ反映しています。(GetPositionFromRedis.cs)
