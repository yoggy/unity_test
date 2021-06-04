using System;
using System.IO;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int id;
    public string name;
    public string address;

    [System.NonSerialized]
    public string hoge;

    public string ToString()
    {
        return $"id={id}, name={name}, address={address}";
    }
}

[System.Serializable]
public class AddresseBook
{
    public Item[] items;

    public void DebugPrint()
    {
        foreach (var i in items)
        {
            Debug.Log(i.ToString());
        }
    }
}

public class LoadJson : MonoBehaviour
{
    void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "address_book.json");
        string json_str = File.ReadAllText(path);

        try
        {
            AddresseBook addresseBook = JsonUtility.FromJson<AddresseBook>(json_str);
            addresseBook.DebugPrint();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
