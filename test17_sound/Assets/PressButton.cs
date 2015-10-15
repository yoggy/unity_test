using UnityEngine;
using System.Collections;

public class PressButton : MonoBehaviour {

    public AudioSource targetAudioSource;


    public void OnClick()
    {
        targetAudioSource.Play();
    }

}
