using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Mirror;

public class PlayerScript : NetworkBehaviour
{
    public GameObject spawn_prefab;
    public TextMesh text_mesh;
    public float speed = 3.0f;
    NavMeshAgent nav_mesh_agent;

    void Start()
    {
        NetworkIdentity network_identity = GetComponent<NetworkIdentity>();
        text_mesh.text = "Player " + network_identity.netId;
    }

    public override void OnStartLocalPlayer()
    {
        nav_mesh_agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (isLocalPlayer == false) return;

        if (Input.GetKeyDown("space"))
        {
            CmdSpawnCube(transform.position, transform.rotation);
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 d = new Vector3(h, 0, v);

        nav_mesh_agent.Move(d * (Time.deltaTime * speed));
    }

    [Command]
    void CmdSpawnCube(Vector3 p, Quaternion q)
    {
        GameObject obj = Instantiate(spawn_prefab, p + Vector3.up * 3, q);
        NetworkServer.Spawn(obj);
    }
}
