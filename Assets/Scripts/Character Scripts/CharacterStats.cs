using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    //This script will be added to every object using a stat
    // A scriptable object would be used to get the info of each character

    //State of each character?
    public enum State
    {
        IDLE = 0,
        MOVE = 1,
        BATTLE = 2,
        DEAD = 3
    }

    public NavMeshAgent agent;
    public NavMeshObstacle obstacle;

    public State state;

    [SerializeField] private Animator animator;

    private int PARAM_STATE = Animator.StringToHash("State");

    public float maxHealth;
    [SerializeField] private float health;
    public float movementSpeed;
    public Action OnDeathEvent;

    public Slider healthBar;

    private void Start()
    {
        health = maxHealth;

        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        agent.speed = movementSpeed;
        SetState(State.IDLE);
    }

    public void SetState(State state)
    {
        this.state = state;

        if (animator != null && animator.gameObject.activeInHierarchy)
            animator.SetInteger(PARAM_STATE, (int)state);

        if(state == State.MOVE)
        {
            CharacterMove();
        }
        else
        {
            TurnAgentOff();
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.value = health;

        if (health <= 0)
        {
            SetState(State.DEAD);
            OnDeathEvent?.Invoke();
        }
    }

    private void CharacterMove()
    {
        agent.enabled = true;
        if (obstacle != null)
        {
            obstacle.enabled = true;
        }
        agent.isStopped = false;
    }

    public void SetTargetLocation(Vector3 targetLocation)
    {
        //Only used to set the location of the agent
        agent.SetDestination(targetLocation);
    }

    private void TurnAgentOff()
    {
        if(agent.enabled)
            agent.isStopped = true;

        agent.enabled = false;
        if (obstacle != null)
        {
            obstacle.enabled = false;
        }
    }
}
