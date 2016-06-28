using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MQTTSample : MonoBehaviour {

    MQTTClient client = new MQTTClient();

    void Start ()
    {
        client.Connect("test.mosquitto.org");
        StartCoroutine("Loop");
    }

    IEnumerator Loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            Publish();
        }
    }

    void Publish()
    {
        client.Publish("test/test24_headless", System.DateTime.Now.ToString());
    }
}
