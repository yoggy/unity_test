using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public float remaining_time = 3.0f;

    Text text;

    string message;
    float st;

    public void SetMessage(string message)
    {
        text = GetComponent<Text>();

        st = Time.time;
        this.message = message;

        Debug.Log(message);
    }

    void Update()
    {
        float diff = Time.time - st;
        if (diff <= remaining_time)
        {
            gameObject.SetActive(true);
            text.text = message;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
