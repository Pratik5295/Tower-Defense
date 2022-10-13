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

    [SerializeField] private float thresholdDistance;
    private float attackCounter;
    [SerializeField] private float maxAttackCounter;
    private void Start()
    {
        agent.speed = stats.movementSpeed;
        target = GameObject.FindGameObjectWithTag("TownHall");
        if (target == null) return;
       
        SetTarget(target);

        stats.OnDeathEvent += OnDeathEventHandler;
    }

    private void Update()
    {
        HasReachDestination();
        Battle();
    }

    private void OnDestroy()
    {
        stats.OnDeathEvent -= OnDeathEventHandler;
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;
        agent.SetDestination(target.transform.position);
        stats.SetState(CharacterStats.State.MOVE);
    }

    private void OnDeathEventHandler()
    {
        OnEnemyDeathEvent?.Invoke();

        if(CurrencyManager.Instance != null)
            CurrencyManager.Instance.AddAmount(bounty);
        
        Destroy(this.gameObject);
    }

    private void HasReachDestination()
    {
        if (target == null) return;

        if (stats.state == CharacterStats.State.BATTLE) return;

        float distance = Vector3.Distance(target.transform.position, this.transform.position);

        if(distance < thresholdDistance)
        {
            agent.isStopped = true;
            stats.SetState(CharacterStats.State.BATTLE);
        }
    }

    private void Battle()
    {
        if (target == null) return;

        if(stats.state == CharacterStats.State.BATTLE)
        {
            attackCounter -= Time.deltaTime;

            if(attackCounter < 0)
            {
                Debug.Log($"{this.gameObject.name} is attacking {target.name}");
                attackCounter = maxAttackCounter;
            }
        }
    }
}
