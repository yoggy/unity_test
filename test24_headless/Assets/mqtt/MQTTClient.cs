using UnityEngine;
using System;
using System.Collections;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

// see also... https://m2mqtt4unity.codeplex.com/documentation

public class MQTTClient {
    private MqttClient4Unity client;
    private string clientId;


    public MQTTClient()
    {
        clientId = "UnityMQTT" + Guid.NewGuid().ToString();
    }

    public bool IsConnected
    {
        get {
            if (client == null) return false;

            bool rv = client.IsConnected;
            if (rv == false)
            {
                Disconnect();
            }

            return rv;
        }
    }

    public bool Connect(string host, int port = 1883, string username = null, string password = null)
    {
        Disconnect();

        try
        {
            client = new MqttClient4Unity(host, port, false, null);
            if (username != null)
            {
                client.Connect(clientId, username, password);
            }
            else
            {
                client.Connect(clientId);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            return false;
        }
        return true;
    }

    public void Disconnect()
    {
        if (client != null)
        {
            client.Disconnect();
            client = null;
        }
    }

    public bool Publish(string topic, string message)
    {
        if (IsConnected == false) return false;

        try
        {
            // topic, message, qos, retain
            client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message), 0, false);
        }
        catch (Exception e)
        {
            Debug.LogError(e.ToString());
            return false;
        }
        return true;
    }

    public bool Subscribe(string topic)
    {
        if (IsConnected == false) return false;
        client.Subscribe(topic);
        return true;
    }

    public bool Unsubscribe(string topic)
    {
        if (IsConnected == false) return false;
        client.Unsubscribe(new string[1] {topic});
        return true;
    }

    public int Count()
    {
        if (IsConnected == false) return 0;
        return client.Count();
    }

    public string Receive()
    {
        string message = client.Receive();
        return message;
    }
}
