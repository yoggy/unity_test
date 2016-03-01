using UnityEngine;
using System.Collections;

public class GetPositionFromRedis : MonoBehaviour {

    Redis redis;

	void Start () {
        redis = Redis.GetInstance();
	}
	
	void Update () {
        float x = 1.0f;
        float y = redis.getFloat("y");
        float z = 0.0f;

        gameObject.transform.localPosition = new Vector3(x, y, z);
    }
}
