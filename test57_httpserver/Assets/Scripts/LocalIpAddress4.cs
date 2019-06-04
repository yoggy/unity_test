using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class LocalIpAddress4 : MonoBehaviour
{
    public string ip_address = "0.0.0.0";
    public float update_interval = 3.0f;

    float total_dulation = 0.0f;

    void Start()
    {
        ip_address = GetLocalIpAddress4();
    }

    void Update()
    {
        total_dulation += update_interval;
        if (total_dulation >= update_interval)
        {
            ip_address = GetLocalIpAddress4();
            total_dulation = 0.0f;
        }
    }

    public string GetLocalIpAddress4()
    {
        string hostname = Dns.GetHostName();
        IPAddress[] addrs = Dns.GetHostAddresses(hostname);

        List<string> strs = new List<string>();

        foreach (IPAddress addr in addrs)
        {
            if (addr.ToString().IndexOf("169.254") == 0) continue;
            if (addr.ToString().IndexOf(":") >= 0) continue; // ignore ipv6 address
            strs.Add(addr.ToString());
        }

        string str = "";
        for (var i = 0; i < strs.Count; ++i)
        {
            str += strs[i];
            if (i < strs.Count - 1)
            {
                str += ", ";
            }
        }

        return str;
    }
}
