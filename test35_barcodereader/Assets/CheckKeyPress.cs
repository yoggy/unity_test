using System;
using UnityEngine;
using UnityEngine.UI;

public class CheckKeyPress : MonoBehaviour {


    [SerializeField]
    Text text;

    [SerializeField]
    ParticleSystem particle_system;

    [SerializeField]
    AudioSource audio_source;

    string msg = "";
    DateTime st;

    void Start()
    {
        st = DateTime.Now;
        particle_system.Stop();
    }

    void Update()
    {
        KeyCode BluetoothReturn = (KeyCode)10; // see also...http://answers.unity3d.com/questions/1277489/how-to-detect-keycodereturn-on-android-bluetooth-k.html

        foreach (char c in Input.inputString)
        {
            if (c == (int)KeyCode.Return || c == (int)BluetoothReturn)
            {
                text.text = msg.ToUpper();
                msg = "";

                text.color = new Color(1, 1, 1, 1);
                st = DateTime.Now;
                particle_system.Play();

                audio_source.Play();
            }
            else
            {
                msg += c.ToString();
            }
        }

        TimeSpan diff = DateTime.Now - st;
        if (diff.TotalMilliseconds > 500)
        {
            if (particle_system.isPlaying == true)
            {
                particle_system.Stop();
            }
        }

        if (diff.TotalMilliseconds < 2000)
        {
            double a = 1.0 - (diff.TotalMilliseconds / 2000.0);
            text.color = new Color(1, 1, 1, (float)a);
        }
        else
        {
            text.text = "";
        }

    }
}
