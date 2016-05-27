using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MQTTSample : MonoBehaviour {

    public InputField host_object;
    public InputField port_object;
    public InputField subscribe_topic_object;
    public InputField publish_topic_object;
    public InputField publish_message_object;
    public Text received_message_object;

    MQTTClient client = new MQTTClient();

    ArrayList messages = new ArrayList();

    void Start () {
	
	}
	
	void Update () {
        while (client.Count() > 0)
        {
            string message = client.Receive();

            Debug.Log("Receive : " + message);

            messages.Add(message);
            if (messages.Count > 12)
            {
                messages.RemoveAt(0);
            }

            string text = "";
            foreach (string m in messages)
            {
                text = text +  m + "\n";
            }

            received_message_object.text = text;
        }
	}

    public void Connect()
    {
        client.Disconnect();

        Debug.Log("Connect");

        string host = host_object.text;
        int port = int.Parse(port_object.text);

        client.Connect(host, port);

        string topic = subscribe_topic_object.text;
        client.Subscribe(topic);
    }

    public void Disconnect()
    {
        Debug.Log("Disconnect");
        client.Disconnect();
    }

    public void Publish()
    {
        Debug.Log("Publish : topic=" + publish_topic_object.text + ", message=" + publish_message_object.text);
        client.Publish(publish_topic_object.text, publish_message_object.text);
    }
}
