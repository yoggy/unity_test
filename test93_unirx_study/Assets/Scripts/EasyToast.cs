using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class EasyToast : MonoBehaviour
{
    public Image panelToast;
    public Text textToast;

    void Awake()
    {
        panelToast.gameObject.SetActive(false);
        MessageBroker.Default.Receive<string>().Subscribe(x => OnReceive(x));
    }

    void OnReceive(string message)
    {
        textToast.text = message;
        panelToast.gameObject.SetActive(true);

        Observable.Timer(TimeSpan.FromSeconds(2))
            .Subscribe(_ => panelToast.gameObject.SetActive(false));
    }

    public static void Show(string message)
    {
        MessageBroker.Default.Publish(message);
    }
}
