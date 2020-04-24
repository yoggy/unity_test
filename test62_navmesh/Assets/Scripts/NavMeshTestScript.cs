using UnityEngine;

public class NavMeshTestScript : MonoBehaviour
{
    public GameObject destination_object;
    public UnityEngine.AI.NavMeshAgent nav_mesh_agent;

    void Update()
    {
        nav_mesh_agent.SetDestination(destination_object.transform.position);
    }
}
