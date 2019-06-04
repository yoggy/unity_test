//
//  TinyHTTPServerForUnity.cs
//
//  github:
//    https://github.com/yoggy/unity_test/test57_httpserver/Assets/Scripts/TinyWebSocketServerForUnity.cs
//
//  libraries:
//    WebSocketSharp https://github.com/sta/websocket-sharp
//
//  license:
//    Copyright(c) 2019 yoggy<yoggy0@gmail.com>
//    Released under the MIT license
//    http://opensource.org/licenses/mit-license.php;
//
using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

[System.Serializable]
public class OnMessage : UnityEvent<string> { }

[DisallowMultipleComponent]
public class TinyWebSocketServerForUnity : SingletonMonoBehaviour<TinyWebSocketServerForUnity>
{
    public int port = 10081;
    public OnLoggingEvent on_logging_event;
    public OnMessage on_message;

    WebSocketServer websocket_server;
    bool loop_flag = true;

    SynchronizationContext serialization_context;

    void Start()
    {
        serialization_context = SynchronizationContext.Current;

        websocket_server = new WebSocketServer(port);
        websocket_server.AddWebSocketService<TinyWebSocketClient>("/");
        websocket_server.Start();
        Debug.Log("websocket_server.Start()");
    }

    void OnDestroy()
    {
        websocket_server.Stop();
        Debug.Log("websocket_server.Stop()");
    }

    public static void Log(string msg)
    {
        var websocketserver = TinyWebSocketServerForUnity.Instance;
        websocketserver.serialization_context.Post(__ =>
        {
            Debug.Log(msg);
            websocketserver.on_logging_event.Invoke("ws", msg);
        }, null);
    }

    public static void OnMessage(string msg)
    {
        var websocketserver = TinyWebSocketServerForUnity.Instance;
        websocketserver.serialization_context.Post(__ =>
        {
            Debug.Log(msg);
            websocketserver.on_message.Invoke(msg);
        }, null);
    }

    public void BroadcastMessage(string msg)
    {
        serialization_context.Post(__ =>
        {
            Debug.Log("TinyWebSocketServerForUnity.BroadcastMessage msg=" + msg);
            websocket_server.WebSocketServices.Broadcast(msg);
        }, null);
    }
}

public class TinyWebSocketClient : WebSocketBehavior
{
    protected override void OnOpen()
    {
        TinyWebSocketServerForUnity.Log("TinyWebSocketClient.OnOpen() uri=" + Context.RequestUri);
    }

    protected override void OnMessage(MessageEventArgs evt)
    {
        TinyWebSocketServerForUnity.Log("TinyWebSocketClient.OnMessage : evt.Data=" + evt.Data);
        TinyWebSocketServerForUnity.OnMessage(evt.Data);
    }

    protected override void OnClose(CloseEventArgs evt)
    {
        TinyWebSocketServerForUnity.Log("TinyWebSocketClient.OnClose : evt=" + evt);
    }

    protected override void OnError(ErrorEventArgs evt)
    {
        TinyWebSocketServerForUnity.Log("TinyWebSocketClient.OnError : evt=" + evt);
    }
}