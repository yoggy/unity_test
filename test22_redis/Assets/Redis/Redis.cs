//
// Redis.cs - How to use Redis in Unity
//
// TeamDev C# Redis Client
// https://redis.codeplex.com/
//
using UnityEngine;
using System.Collections;
using System;
using TeamDev.Redis;

public class Redis {

    //
    // singletone
    //
    static Redis singleton;

    public static Redis GetInstance()
    {
        if (singleton == null)
        {
            singleton = new Redis();
        }

        return singleton;
    }

    ////////////////////////////////////////////////////////////////

    protected RedisDataAccessProvider redis;

    protected Redis()
    {
        try
        {
            redis = new RedisDataAccessProvider();
            redis.Configuration.Host = "127.0.0.1";
            redis.Configuration.Port = 6379;
            redis.Connect();
        }
        catch(Exception e)
        {
            Debug.Log("Redis connection failed..." + e.Message);
            redis = null;
        }
    }

    public bool isConnected()
    {
        if (redis == null) return false;
        return true;
    }

    public float getFloat(string key)
    {
        string str = getString(key);
        if (str == null) return 0.0f;
        return float.Parse(str);
    }

    public void setFloat(string key, float val)
    {
        setString(key, val.ToString());
    }

    public string getString(string key)
    {
        if (redis == null) return null;

        int command_id = redis.SendCommand(RedisCommand.GET, key);
        string str = redis.ReadString(command_id);

        if (str == null) return null;

        return str;
    }

    public void setString(string key, string val)
    {
        if (redis == null) return;

        redis.SendCommand(RedisCommand.SET, key, val);
        redis.WaitComplete();
    }
}
