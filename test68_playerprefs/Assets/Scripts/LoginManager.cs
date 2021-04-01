using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public InputField input_username;
    public InputField input_password;

    public const string KEY_USERNAME = "username";
    public const string KEY_PASSWORD = "password";

    void Start()
    {
        input_username.text = PlayerPrefs.GetString(KEY_USERNAME, "");
        input_password.text = PlayerPrefs.GetString(KEY_PASSWORD, "");
    }

    public void OnPressLoginButton()
    {
        PlayerPrefs.SetString(KEY_USERNAME, input_username.text);
        PlayerPrefs.SetString(KEY_PASSWORD, input_password.text);
    }
}
