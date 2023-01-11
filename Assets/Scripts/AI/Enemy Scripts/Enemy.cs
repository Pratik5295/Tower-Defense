using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Units;

public static partial class Units
{
    public enum UNIT_TYPE
    {
        TOWNHALL = 0,
        HERO = 1,
        TOWER = 2,
        ENEMY = 3
    }
}
public class Enemy : MonoBehaviour
{
    //One hit destroy enemies for now. To be expanded later
    [Header("List of Targets")]
    //List of all targets
    [SerializeField] protected GameObject currentTarget;
    [SerializeField] protected List<GameObject> potentialTargets;
    [SerializeField] protected GameObject townHall;
    [SerializeField] protected Hero hero;

    [Space(5)]
    [Header("Character Stats")]
    public CharacterStats stats;
  
    [SerializeField] private bool isRanged;
    public int bounty;      //Amount of money player receives for kill

    [SerializeField] private float damage;
    [SerializeField] protected Vector3 targetLocation;

    [SerializeField] private float thresholdDistance;
    [SerializeField] private float attackCounter;
    [SerializeField] private float maxAttackCounter;

    [SerializeField] private float distance;


    public Action OnEnemyDeathEvent;

    private void Start()
    {
        stats.OnDeathEvent += OnDeathEventHandler;
        //SetTarget(target);
        currentTarget = GameObject.FindGameObjectWithTag("TownHall");
        if (currentTarget == null) return;
        AddTarget(currentTarget, UNIT_TYPE.TOWNHALL);
    }

    private void Update()
    {
        if (currentTarget == null)
        {
            SearchForTarget();
        }
        else
        {
            HasReachDestination();
        }
    }

    private void OnDestroy()
    {
        stats.OnDeathEvent -= OnDeathEventHandler;

        if (hero != null)
        {
            hero.OnDeathEvent -= SetTargetToHall;
        }
    }

    public void AddTarget(GameObject _target, UNIT_TYPE type)
    {
        switch (type)
        {
            case UNIT_TYPE.TOWNHALL:
                townHall = _target;
                break;
            case UNIT_TYPE.HERO:
                hero = _target.GetComponent<Hero>();
                break;
            case UNIT_TYPE.TOWER:
                if (potentialTargets.Contains(_target)) return;
                potentialTargets.Add(_target);
                break;
        }
        GetPriorityTarget();
    }

    public void RemoveTarget(GameObject _target)
    {
        if(_target.GetComponent<Hero>() != null)
        {
            hero = null;
            SearchForTarget();
            return;
        }
        if (potentialTargets.Contains(_target)) potentialTargets.Remove(_target);
    }

    public virtual void SetTarget(GameObject _target)
    {
        //Priority for targets
        // 1 - Hero
        // 2 - Tower
        // 3 - Townhall
        if (currentTarget == null)
        {
            hero = null;
            stats.SetState(CharacterStats.State.IDLE);
            return;
        }
        targetLocation = currentTarget.transform.position;
        stats.SetState(CharacterStats.State.MOVE);
        stats.SetTargetLocation(targetLocation);
    }

    public void SearchForTarget()
    {
        GetPriorityTarget();

        if (currentTarget != null)
        {
            SetTarget(currentTarget);
        }

    }

    public void SetTargetToHall()
    {
        if (hero != null)
        {
            hero.OnDeathEvent -= SetTargetToHall;
            hero = null;
        }

        currentTarget = currentTarget = GameObject.FindGameObjectWithTag("TownHall");
        if (currentTarget == null)
        {
            stats.SetState(CharacterStats.State.IDLE);
            return;
        }
        stats.SetState(CharacterStats.State.MOVE);
        stats.SetTargetLocation(targetLocation);
    }

    private void OnDeathEventHandler()
    {
        OnEnemyDeathEvent?.Invoke();

        if (CurrencyManager.Instance != null)
            CurrencyManager.Instance.AddAmount(bounty);

        Destroy(this.gameObject);
    }

    private void BattleAttackAnimation()
    {
        if (stats.state == CharacterStats.State.BATTLE)
        {
            Vector3 targetRotation = new Vector3(currentTarget.transform.position.x,
                                                        transform.position.y,
                                                         currentTarget.transform.position.z);
            transform.LookAt(targetRotation);
            return;
        }
    }
    private void HasReachDestination()
    {
        if (currentTarget == null) return;

        BattleAttackAnimation();

        if (targetLocation != currentTarget.transform.position)
        {
            //Target is moving
            targetLocation = currentTarget.transform.position;
            stats.SetTargetLocation(targetLocation);
        }

        distance = Vector3.Distance(currentTarget.transform.position, this.transform.position);

        if (distance < thresholdDistance)
        {
            Vector3 targetRotation = new Vector3(currentTarget.transform.position.x,
                                                        transform.position.y,
                                                         currentTarget.transform.position.z);
            transform.LookAt(targetRotation);

            if (stats.state == CharacterStats.State.BATTLE) return;
            stats.SetState(CharacterStats.State.BATTLE);
        }
        else
        {
            targetLocation = currentTarget.transform.position;
            stats.SetState(CharacterStats.State.MOVE);
            stats.SetTargetLocation(targetLocation);
        }
    }


    public void Battle()
    {
        if (currentTarget == null) return;

        CharacterStats targetStats = currentTarget.GetComponent<CharacterStats>();

        if (targetStats != null)
            targetStats.TakeDamage(damage);


        Tower tower = currentTarget.GetComponent<Tower>();

        if (tower != null)
            tower.TakeDamage(damage);
    }

    public void TakeDamage(float amount)
    {
        stats.TakeDamage(amount);
    }

    protected virtual void GetPriorityTarget()
    {
        ///<summary>
        /// This function would be overridden by all enemy classes
        /// In return it will get the gameobject with top priority to be set as target
        /// </summary>
        /// 

        if(hero != null)
        {
            currentTarget = hero.gameObject;

            if (currentTarget.GetComponent<Hero>() != null)
            {
                hero = currentTarget.GetComponent<Hero>();
                hero.OnDeathEvent += SetTargetToHall;
            }

            return;
        }
        else if(townHall != null)
        {
            currentTarget = townHall;
            return;
        }
    }
}
