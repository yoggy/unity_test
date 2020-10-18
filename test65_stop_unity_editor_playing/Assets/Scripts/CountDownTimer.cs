using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    Text text;
    float duration = 4.0f;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        float t = duration - Time.time;
        if (t <= 0.0f) t = 0.0f;
        text.text = $"{t:F2}";

        if (t == 0.0f)
        {
            // Unityエディタでのプレビュー再生を停止する
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
