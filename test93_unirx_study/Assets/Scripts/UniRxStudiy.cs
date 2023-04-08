using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UniRxStudiy : MonoBehaviour
{
    public Button targetButton;

    void Start()
    {
        targetButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromSeconds(3)).Subscribe(_ => OnPressButton());
    }

    void OnPressButton()
    {
        DateTime date = DateTime.Now;
        string datestr = date.ToString("yyyy-MM-ddTHH:mm:sszzzz");
        EasyToast.Show(datestr);
    }
}
