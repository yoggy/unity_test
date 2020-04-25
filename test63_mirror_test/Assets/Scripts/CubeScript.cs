using UnityEngine;
using Mirror;

public class CubeScript : NetworkBehaviour
{
    public float lifetime = 30.0f;

    void Start()
    {
        if (isServer == false)
        {
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            GameObject.Destroy(rigidbody);
        }
    }

    public override void OnStartServer()
    {
        Debug.Log("CubeScript.OnStartServer()");

        base.OnStartServer();

        Invoke(nameof(DestroySelf), lifetime);
    }

    [Server]
    void DestroySelf()
    {
        Debug.Log("CubeScript.DestroySelf()");
        NetworkServer.Destroy(gameObject);
    }
}

