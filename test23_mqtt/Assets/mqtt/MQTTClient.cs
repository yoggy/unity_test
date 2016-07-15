using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

// see also... https://m2mqtt4unity.codeplex.com/documentation

public class MQTTMessage
{
    public string topic;
    public string message;

    override public string ToString()
    {
        string str;
        str = "{topic=" + topic + ", message=" + message + "}";
        return str;
    }
}

public class MQTTClient {
    private MqttClient4Unity client;
    private string clientId;

    private Queue messageQueue = new Queue();

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
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
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
        lock (messageQueue)
        {
            return messageQueue.Count;
        }
    }

    void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
    {
        Debug.Log(e.ToString());
        lock (messageQueue)
        {
            MQTTMessage msg = new MQTTMessage();
            msg.topic = e.Topic.ToString();
            msg.message = System.Text.Encoding.UTF8.GetString(e.Message);
            messageQueue.Enqueue(msg);
        }
    }

    public bool IsMessageArrived()
    {
        lock (messageQueue)
        {
            if (messageQueue.Count == 0) return false;
            return true;
        }
    }

    public MQTTMessage Receive()
    {
        lock (messageQueue)
        {
            if (messageQueue.Count == 0) return null;
            MQTTMessage msg = (MQTTMessage)messageQueue.Dequeue();
            return msg;
        }
    }
}
