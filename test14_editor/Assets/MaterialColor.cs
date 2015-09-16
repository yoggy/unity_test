using UnityEngine;
using System.Collections;

public class MaterialColor : MonoBehaviour {

    [Header("マテリアル色の設定")]

    [ColorUsage(false)]    // アルファ値なし
    public Color colorRGB = new Color(1, 1, 1);

    [Header("以下の色の設定はダミーです")]

    [ColorUsage(true)]    // アルファ値あり
    public Color colorRGBA;

    [ColorUsage(true, true, 0, 3, 0, 1)] // showAlpha, HDR, minBrightnessValue, maxBrightnessValue, minExposureValue, maxExposuerValue 
    public Color colorHDR;

    Material material;

    void Start () {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        material = renderer.material;
	}
	
	void Update () {
        material.color = colorRGB;
	}

}
