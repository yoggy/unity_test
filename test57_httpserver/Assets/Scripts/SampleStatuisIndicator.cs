using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogEntry
{
    public long t;
    public string log;

    public LogEntry()
    {
        this.t = GetTickTime();
        this.log = "";
    }

    public LogEntry(string log)
    {
        this.t = GetTickTime();
        this.log = log;
    }

    public static long GetTickTime()
    {
        return (long)(Time.time * 1000);
    }
}

public class SampleStatuisIndicator : MonoBehaviour
{
    public TinyHTTPServerForUnity http_server;
    public Text text_port;
    public Text text_message;

    List<LogEntry> logs = new List<LogEntry>();

    void Start()
    {
        text_port.text = http_server.port.ToString();
        UpdateMessages();
    }

    void Update()
    {
        CheckExpire();
    }

    public void OnLoggingEvent(string method, string url)
    {
        var str = string.Format("{0:D6} : {1} {2}", LogEntry.GetTickTime(), method, url);
        AddLog(str);
    }

    public void AddLog(string str)
    {
        logs.Add(new LogEntry(str));

        if (logs.Count > 10)
        {
            logs.RemoveAt(0);
        }

        UpdateMessages();
    }

    void UpdateMessages()
    {
        string str = "";
        foreach (var l in logs)
        {
            str += l.log;
            str += "\n";
        }

        text_message.text = str;
    }

    void CheckExpire()
    {
        bool do_remove = false;
        for (int i = logs.Count - 1; i >= 0; --i)
        {
            var l = logs[i];
            if (LogEntry.GetTickTime() - l.t > 7000)
            {
                logs.RemoveAt(i);
                do_remove = true;
            }
        }
        if (do_remove == true)
        {
            UpdateMessages();
        }
    }
}
