using UnityEngine;
using UnityEngine.UI;

public class RawImageUpdater : MonoBehaviour {

    public RawImage raw_image;
    Texture2D texture;

    void Start () {
        texture = new Texture2D(128, 128, TextureFormat.RGB24, false);

        // RawImageの色は必ず白に設定すること。黒だと表示されない
        raw_image.color = Color.white; 

        // RawImageで表示するテクスチャを設定する
        raw_image.texture = texture;
	}

    float rand() {
        return Random.Range(0.0f, 1.0f);
    }

	void Update () {
        // 適当にテクスチャの内容を更新する
        for (int y = 0; y < texture.height; ++y)
        {
            for (int x = 0; x < texture.width; ++x)
            {
                Color c = new Color(rand(), rand(), rand()); // 値の範囲は0.0～1.0f。255を設定すると白とびする
                texture.SetPixel(x, y, c);
            }
        }

        // テクスチャの内容を変更した場合はApply()を実行してGPU側のテクスチャメモリへ反映すること
        texture.Apply(); 
	}
}
