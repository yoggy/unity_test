using UnityEngine;
using System.Collections;

public class SetPositionToRedis : MonoBehaviour {

    Redis redis;

    void Start()
    {
        redis = Redis.GetInstance();
    }

    void Update()
    {
        Vector2 pos = gameObject.transform.localPosition;
        redis.setFloat("y", pos.y);
    }
}
