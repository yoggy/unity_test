using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ReadTextFile : MonoBehaviour
{
    public Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    public void ReadFile(string path)
    {
        string str = File.ReadAllText(path);
        text.text = str;
    }
}
