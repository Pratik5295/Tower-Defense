using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //One hit destroy enemies for now. To be expanded later
    public NavMeshAgent agent;
    [SerializeField]private GameObject target;

    public CharacterStats stats;
    public Action OnEnemyDeathEvent;
    public int bounty;      //Amount of money player receives for kill
    private void Start()
    {
        agent.speed = stats.movementSpeed;
        //target = GameObject.FindGameObjectWithTag("TownHall");
        if (target == null) return;
        agent.SetDestination(target.transform.position);
        stats.OnDeathEvent += OnDeathEventHandler;
    }

    private void OnDestroy()
    {
        stats.OnDeathEvent -= OnDeathEventHandler;
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
        agent.SetDestination(target.transform.position);
    }

    private void OnDeathEventHandler()
    {
        OnEnemyDeathEvent?.Invoke();

        if(CurrencyManager.Instance != null)
            CurrencyManager.Instance.AddAmount(bounty);
        
        Destroy(this.gameObject);
    }
}
