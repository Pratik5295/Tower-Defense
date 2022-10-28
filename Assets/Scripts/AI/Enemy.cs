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
    [SerializeField]private Hero hero;
    public int bounty;      //Amount of money player receives for kill

    [SerializeField] private float damage;
    [SerializeField] private Vector3 targetLocation;

    [SerializeField] private float thresholdDistance;
    [SerializeField] private float attackCounter;
    [SerializeField] private float maxAttackCounter;
    private void Start()
    {
        stats.OnDeathEvent += OnDeathEventHandler;
        agent.speed = stats.movementSpeed;
        target = GameObject.FindGameObjectWithTag("TownHall");
        if (target == null) return;
       
        SetTarget(target);
    }

    private void Update()
    {
        HasReachDestination();
    }

    private void OnDestroy()
    {
        stats.OnDeathEvent -= OnDeathEventHandler;
    }

    public void SetTarget(GameObject _target)
    {
        target = _target;

        if (target == null)
        {
            agent.isStopped = true;
            stats.SetState(CharacterStats.State.IDLE);
            return;
        }
        targetLocation = target.transform.position;
        agent.SetDestination(targetLocation);
        stats.SetState(CharacterStats.State.MOVE);

        if(target.GetComponent<Hero>() != null)
        {
            hero = target.GetComponent<Hero>();
            hero.OnDeathEvent += SetTargetToHall;
        }
    }

    public void SetTargetToHall()
    {
        if (hero != null)
        {
            hero.OnDeathEvent -= SetTargetToHall;
            hero = null;
        }

        target = target = GameObject.FindGameObjectWithTag("TownHall");
        if (target == null) 
        {
            agent.isStopped = true;
            stats.SetState(CharacterStats.State.IDLE);
            return; 
        }

        agent.SetDestination(target.transform.position);
        stats.SetState(CharacterStats.State.MOVE);
    }

    private void OnDeathEventHandler()
    {
        Debug.Log("Dying enemy");
        OnEnemyDeathEvent?.Invoke();

        if(CurrencyManager.Instance != null)
            CurrencyManager.Instance.AddAmount(bounty);
        
        Destroy(this.gameObject);
    }

    private void HasReachDestination()
    {
        if (target == null) return;

        if (stats.state == CharacterStats.State.BATTLE) return;

        if (targetLocation != target.transform.position)
        {
            //Target is moving
            targetLocation = target.transform.position;
            agent.SetDestination(targetLocation);
        }

        float distance = Vector3.Distance(targetLocation, this.transform.position);

        if(distance < thresholdDistance)
        {
            agent.isStopped = true;
            
            if (attackCounter == 0)
            {
                agent.isStopped = true;
                stats.SetState(CharacterStats.State.BATTLE);
                attackCounter = maxAttackCounter;
            }
        }
        else
        {
            agent.isStopped = false;
            targetLocation = target.transform.position;
            agent.SetDestination(targetLocation);
            stats.SetState(CharacterStats.State.MOVE);
        }
    }

    public void Battle()
    {
        if (target == null) return;

        //if(stats.state == CharacterStats.State.BATTLE)
        //{
        //    attackCounter -= Time.deltaTime;

        //    if(attackCounter < 0)
        //    {
        //        Debug.Log($"{this.gameObject.name} is attacking {target.name}");
        //        attackCounter = maxAttackCounter;
        //    }
        //}

        Debug.Log($"{target.gameObject.name}");
        CharacterStats targetStats = target.GetComponent<CharacterStats>();

        if (targetStats != null)
            targetStats.TakeDamage(damage);
    }
}
