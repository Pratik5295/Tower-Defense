using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;

    private void Start()
    {
        agent.SetDestination(target.transform.position);
    }
}
