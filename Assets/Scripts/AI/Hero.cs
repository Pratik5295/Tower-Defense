using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(NavMeshAgent))]
public class Hero : MonoBehaviour
{
    public CharacterStats characterStats;

    [SerializeField] private float damage;
    [SerializeField] private float maxDamage;
    public Action OnDeathEvent;

    [SerializeField] private float thresholdDistance;
    [SerializeField] private float maxAttackCounter;

    public GameObject heroUI;   //On death turn inactive

    [Header("Target Details")]
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 targetLocation;

    [SerializeField] private List<GameObject> potentialTargets;


    //Storage variables

    private float originalDamage;
    private void Start()
    {
        potentialTargets = new List<GameObject>();


        originalDamage = damage;

        maxDamage = damage * 2; //For now

        characterStats.OnDeathEvent += OnDeathEventHandler;

        heroUI.SetActive(true);
    }

    private void OnDeathEventHandler()
    {
        OnDeathEvent?.Invoke();

        //Unsubscribe from all event listeners
        UnSubscribeFromAllEvents();

        heroUI.SetActive(false);
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        characterStats.OnDeathEvent -= OnDeathEventHandler;
    }
    public void AddTarget(GameObject enemy)
    {
        if (potentialTargets.Contains(enemy)) return;

        potentialTargets.Add(enemy);
    }

    public void RemoveTarget(GameObject enemy)
    {
        if (!potentialTargets.Contains(enemy)) return;

        potentialTargets.Remove(enemy);


        if (target.GetComponent<Enemy>() != null)
        {
            Enemy enem = target.GetComponent<Enemy>();
            enem.OnEnemyDeathEvent -= OnEnemyDeathEventListener;
        }

        target = null;
        targetLocation = Vector3.zero;
        characterStats.SetState(CharacterStats.State.IDLE);
    }

    //Listening to if enemy dies
    private void OnEnemyDeathEventListener()
    {
        if (target == null) return;
        Debug.Log($"{target.gameObject.name} has died");
        Enemy enem = target.GetComponent<Enemy>();
        int killBounty = enem.bounty;
        enem.OnEnemyDeathEvent -= OnEnemyDeathEventListener;
        RemoveTarget(target);
    }

    private void Update()
    {
        if (target != null)
        {
            //Has target, do movement and other logic
            OnTargetMovement();
        }
        else
        {
            //Find target
            if (potentialTargets.Count > 0)
            {
                //Set target as our current target got away or died
                if (potentialTargets[0] == null)
                {
                    potentialTargets.RemoveAt(0);
                }
                else
                {
                    SetTarget(potentialTargets[0]);
                }
            }
        }
    }

    private void OnTargetMovement()
    {
        if (characterStats.state != CharacterStats.State.BATTLE)
        {
            if (targetLocation != target.transform.position)
            {
                //Target is moving
                targetLocation = target.transform.position;
                characterStats.SetTargetLocation(targetLocation);
            }
        }

        float distance = Vector3.Distance(target.transform.position, this.transform.position);

        if (distance < thresholdDistance)
        {

            if (target.GetComponent<Enemy>())
            {
                Vector3 targetRotation = new Vector3(target.transform.position.x, 
                                                        transform.position.y, 
                                                         target.transform.position.z);
                transform.LookAt(targetRotation);
                
                if (characterStats.state == CharacterStats.State.BATTLE) return;
                characterStats.SetState(CharacterStats.State.BATTLE);
            }
            else
            {
                if (characterStats.state == CharacterStats.State.IDLE) return;
                characterStats.SetState(CharacterStats.State.IDLE);
                target = null;
            }

        }
        else
        {
            targetLocation = target.transform.position;
            characterStats.SetState(CharacterStats.State.MOVE);
            characterStats.SetTargetLocation(targetLocation);
        }
    }

    public void SetTarget(GameObject _target)
    {
        if (target != null) return;
        if (_target == null) return;
        target = _target;

        targetLocation = target.transform.position;
        
        characterStats.SetState(CharacterStats.State.MOVE);
        characterStats.SetTargetLocation(targetLocation);

        if (target.GetComponent<Enemy>())
        {
            Enemy enem = target.GetComponent<Enemy>();
            enem.OnEnemyDeathEvent += OnEnemyDeathEventListener;
        }
    }

    public void UnSubscribeFromAllEvents()
    {
        foreach(GameObject enem in potentialTargets)
        {
            Enemy target = enem.GetComponent<Enemy>();
            target.OnEnemyDeathEvent -= OnEnemyDeathEventListener;
        }
    }

    public void MoveHero(GameObject _target)
    {
        RemoveTarget(target);
        if (_target == null) return;
        target = _target;

        targetLocation = target.transform.position;
        characterStats.SetState(CharacterStats.State.MOVE);
        characterStats.SetTargetLocation(targetLocation);
    }

    public void Battle()
    {
        CharacterStats targetStats = target.GetComponent<CharacterStats>();
        
        if (targetStats != null)
            targetStats.TakeDamage(damage);
    }

    //For Power and abilities

    public void SetDamage(float value)
    {
        //Damage multiplier
        if (damage >= maxDamage) return;
        damage *= value;
    }

    public void ResetDamage()
    {
        damage = originalDamage;
    }
}
