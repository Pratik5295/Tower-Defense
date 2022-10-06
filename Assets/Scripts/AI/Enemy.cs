using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //One hit destroy enemies for now. To be expanded later
    public NavMeshAgent agent;
    public GameObject target;

    public CharacterStats stats;
    private void Start()
    {
        agent.speed = stats.movementSpeed;
        agent.SetDestination(target.transform.position);
        stats.OnDeathEvent += OnDeathEventHandler;
    }

    private void OnDestroy()
    {
        stats.OnDeathEvent -= OnDeathEventHandler;
    }

    private void OnDeathEventHandler()
    {
        Destroy(this.gameObject);
    }
}
