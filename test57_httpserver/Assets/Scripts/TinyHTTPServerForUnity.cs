//
//  TinyHTTPServerForUnity.cs
//
//  github:
//    https://github.com/yoggy/unity_test/test57_httpserver/Assets/Scripts/TinyHTTPServerForUnity.cs
//
//  license:
//    Copyright(c) 2019 yoggy<yoggy0@gmail.com>
//    Released under the MIT license
//    http://opensource.org/licenses/mit-license.php;
//
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnLoggingEvent : UnityEvent<string, string> { }

[DisallowMultipleComponent]
public class TinyHTTPServerForUnity : SingletonMonoBehaviour<TinyHTTPServerForUnity>
{
    public int port = 10080;
    public OnLoggingEvent on_logging_event = new OnLoggingEvent();

    HttpListener http_listener;
    bool loop_flag = true;

    void Start()
    {
        http_listener = new HttpListener();
        http_listener.Prefixes.Add("http://*:" + port + "/");
        http_listener.Start();
        Listen();
        Debug.Log("HttpListener.Start()");
    }

    void OnDestroy()
    {
        loop_flag = false;
        http_listener.Stop();
        Debug.Log("HttpListener.Stop()");
    }

    public async Task Listen()
    {
        while (loop_flag)
        {
            var context = await http_listener.GetContextAsync();

            // for debug
            Debug.Log("Request : method=" + context.Request.HttpMethod + ", url=" + context.Request.RawUrl);
            on_logging_event.Invoke(context.Request.HttpMethod, context.Request.RawUrl);

            var req = context.Request;
            var res = context.Response;
#if UNITY_ANDROID || !UNITY_EDITOR
            ProcessRequestForAndroid(req, res);
#else
            ProcessRequest(req, res);
#endif
        }
    }

    public void ProcessRequestForAndroid(HttpListenerRequest req, HttpListenerResponse res)
    {
        try
        {
            var url = Application.streamingAssetsPath + req.RawUrl;
            if (url.LastIndexOf("/") == url.Length - 1)
            {
                url += "index.html";
            }

            using (var www = new WWW(url))
            {
                while (!www.isDone)
                {
                }

                if (string.IsNullOrEmpty(www.error))
                {
                    Reply200OKForAndroid(req, res, www);
                    return;
                }
            }

        }
        catch (Exception e)
        {
            Reply500InternalServerError(req, res, e);
        }

        Reply404NotFound(req, res);
    }


    public void ProcessRequest(HttpListenerRequest req, HttpListenerResponse res)
    {
        try
        {
            var local_path = Application.streamingAssetsPath + req.RawUrl;
            var ext = Path.GetExtension(local_path).ToLower();

            if (ext == ".meta")
            {
                Reply404NotFound(req, res);
            }
            else if (Directory.Exists(local_path))
            {
                // check index.html
                var index_html_path = local_path + "/index.html";
                if (File.Exists(index_html_path))
                {
                    Reply200OK(req, res, index_html_path);
                }
                else
                {
                    Reply404NotFound(req, res);
                }
            }
            else if (File.Exists(local_path))
            {
                Reply200OK(req, res, local_path);
            }
            else
            {
                Reply404NotFound(req, res);
            }
        }
        catch (Exception e)
        {
            Reply500InternalServerError(req, res, e);
        }
    }

    Dictionary<string, string> content_type_dict = new Dictionary<string, string>()
    {
        {".html", "text/html"},
        {".htm", "text/html"},
        {".css", "text/css"},
        {".js", "text/javascript"},
        {".csv", "text/csv"},
        {".json", "application/json"},
        {".jpg", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".png", "image/png"},
        {".gif", "image/gif"},
        {".zip", "application/zip"},
    };

    void Reply200OK(HttpListenerRequest req, HttpListenerResponse res, string local_path)
    {
        res.StatusCode = (int)HttpStatusCode.OK;
        var ext = Path.GetExtension(local_path).ToLower();
        if (content_type_dict.ContainsKey(ext))
        {
            res.ContentType = content_type_dict[ext];
        }
        else
        {
            res.ContentType = "application/octet-stream";
        }

        using (var fs = new FileStream(local_path, FileMode.Open, FileAccess.Read))
        {
            byte[] buf = new byte[fs.Length];
            var read_size = fs.Read(buf, 0, (int)fs.Length);

            var os = res.OutputStream;
            os.Write(buf, 0, read_size);
            os.Close();
        }
    }

    void Reply200OKForAndroid(HttpListenerRequest req, HttpListenerResponse res, WWW www)
    {
        res.StatusCode = (int)HttpStatusCode.OK;
        var ext = Path.GetExtension(www.url).ToLower();
        if (content_type_dict.ContainsKey(ext))
        {
            res.ContentType = content_type_dict[ext];
        }
        else
        {
            res.ContentType = "application/octet-stream";
        }

        var os = res.OutputStream;
        os.Write(www.bytes, 0, www.bytes.Length);
        os.Close();
    }

    void Reply404NotFound(HttpListenerRequest req, HttpListenerResponse res)
    {
        res.StatusCode = (int)HttpStatusCode.NotFound;
        res.ContentType = "text/plain";
        using (var writer = new StreamWriter(res.OutputStream, Encoding.UTF8))
        {
            writer.Write("404 not found...req.RawUrl=" + req.RawUrl);
        }
        res.Close();
    }

    void Reply500InternalServerError(HttpListenerRequest req, HttpListenerResponse res, Exception e)
    {
        res.StatusCode = (int)HttpStatusCode.InternalServerError;
        res.ContentType = "text/plain";
        using (var writer = new StreamWriter(res.OutputStream, Encoding.UTF8))
        {
            writer.Write("InternalServerError e=" + e.ToString());
        }
        res.Close();
    }

}
