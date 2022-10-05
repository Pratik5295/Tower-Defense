using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //One hit destroy enemies for now. To be expanded later
    public NavMeshAgent agent;
    public GameObject target;

    private void Start()
    {
        agent.SetDestination(target.transform.position);
    }
}
