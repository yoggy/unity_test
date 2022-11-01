# test88_internet_reachability

```
using UnityEngine;
using UnityEngine.UI;

public class CheckInternetReachability : MonoBehaviour
{
    public Text textInternetReachability;

    void Update()
    {
        // https://docs.unity3d.com/ja/2021.3/ScriptReference/NetworkReachability.html
        switch (Application.internetReachability)
        {
            case NetworkReachability.NotReachable:
                textInternetReachability.text = "NotReachable";
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                textInternetReachability.text = "ReachableViaCarrierDataNetwork";
                break;
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                textInternetReachability.text = "ReachableViaLocalAreaNetwork";
                break;
        }
    }
}
```
