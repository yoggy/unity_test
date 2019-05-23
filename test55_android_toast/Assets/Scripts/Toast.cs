using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
    public int toast_height = 60;

    AndroidJavaClass unity_player_class;
    AndroidJavaObject activity;
    AndroidJavaClass toast_class;

    GameObject toast_image_obj; // image
    Text toast_text;
    float st = -5.0f;

    Queue<string> message_queue = new Queue<string>();

    void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        unity_player_class = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        activity = unity_player_class.GetStatic<AndroidJavaObject>("currentActivity");
        toast_class = new AndroidJavaClass("android.widget.Toast");
#else
        int w = Screen.width;
        int h = Screen.height;

        Canvas canvas = (Canvas)GameObject.FindObjectOfType(typeof(Canvas));
        if (canvas == null)
        {
            Debug.LogError("Please place \"Canvas\" in the scene.");
            return;
        }
        var canvas_rect = canvas.gameObject.GetComponent<RectTransform>();

        toast_image_obj = new GameObject("toast_image_obj");
        toast_image_obj.transform.parent = canvas.gameObject.transform;

        // background image
        var image = toast_image_obj.AddComponent<Image>();
        image.color = new Color(0, 0, 0, 0.25f);

        var image_rect = toast_image_obj.GetComponent<RectTransform>();
        image_rect.localPosition = new Vector3(0, (toast_height * image_rect.localScale.y) / 2);
        image_rect.sizeDelta = new Vector2(w, toast_height);
        image_rect.anchorMin = new Vector2(0.5f, 0);  // you must set anchor value before localPosition...
        image_rect.anchorMax = new Vector2(0.5f, 0);

        // text
        var text_obj = new GameObject("toast_text");
        text_obj.transform.parent = toast_image_obj.transform;

        toast_text = text_obj.AddComponent<Text>();
        toast_text.color = Color.white;
        toast_text.font = (Font)Resources.GetBuiltinResource<Font>("Arial.ttf");
        toast_text.alignment = TextAnchor.MiddleCenter;
        toast_text.fontSize = 16;

        var text_rect = text_obj.GetComponent<RectTransform>();
        text_rect.localPosition = new Vector3(0, 0, 0);
        text_rect.sizeDelta = new Vector2(w, toast_height);

#endif
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Time.time - st > 5.0f)
        {
            // check queue
            if (message_queue.Count > 0)
            {
                toast_text.text = message_queue.Dequeue();
                toast_image_obj.SetActive(true);
                st = Time.time;
            }
            else
            {
                toast_image_obj.SetActive(false);
            }
        }
#endif
    }

    public void ShowImpl(string msg)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            AndroidJavaObject toast = toast_class.CallStatic<AndroidJavaObject>(
                "makeText",
                activity,
                msg,
                toast_class.GetStatic<int>("LENGTH_LONG"));
            toast.Call("show");
        }));
#else
        message_queue.Enqueue(msg);
#endif
    }

    public static void Show(string msg)
    {
        Debug.Log("Toast.show() : msg=" + msg);

        var obj = GameObject.FindObjectOfType(typeof(Toast));
        if (obj == null) return;

        var toast = (Toast)obj;
        toast.ShowImpl(msg);
    }
}
