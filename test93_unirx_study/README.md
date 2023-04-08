# test93_unirx_study
```
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
```
