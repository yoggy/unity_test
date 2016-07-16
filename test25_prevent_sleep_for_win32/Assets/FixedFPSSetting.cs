using UnityEngine;
using System.Collections;

public class FixedFPSSetting : MonoBehaviour {

    [SerializeField]
    int FPS = 30;

    void Awake()
    {
        Application.targetFrameRate = FPS;
    }
}
