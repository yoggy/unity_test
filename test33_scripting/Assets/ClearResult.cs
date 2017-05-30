using UnityEngine;
using UnityEngine.UI;

public class ClearResult : MonoBehaviour {

    [SerializeField]
    InputField inputfield_result;

    public void Clear()
    {
        inputfield_result.text = "";
    }
}
