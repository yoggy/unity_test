test21_skybox - Cylindrical画像をCubemapに使うメモ
====

[img01.png](img01.png)

テクスチャの設定
- Assetにテクスチャを取り込む
- InspectorでTexture TypeをCubemapに設定
- CubemapのMappingを"Latitude-Longitude Layout(Cylindrical)"に設定
- "Apply"ボタンを押して設定を反映する

マテリアルの設定
- マテリアルをAssetに新規追加する
- InspectorでマテリアルのShaderを"Skybox/Cubemap"に設定
- Cubemap(HDR)の項目に先に作成したテクスチャを設定する

ライティングの設定
- メニューの"Window"→"Lighting"を選択して、Lightingウインドを表示する
- Environment LightingのSkyboxの項目に、先に作成したマテリアルを設定する

