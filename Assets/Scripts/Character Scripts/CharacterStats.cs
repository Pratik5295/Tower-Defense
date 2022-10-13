using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;
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

    public State state;

    [SerializeField] private Animator animator;

    private int PARAM_STATE = Animator.StringToHash("State");

    public float maxHealth;
    private float health;
    public float movementSpeed;
    public Action OnDeathEvent;

    public Slider healthBar;

    private void Start()
    {
        health = maxHealth;

        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;

        SetState(State.IDLE);
    }

    public void SetState(State state)
    {
        this.state = state;

        if (animator.gameObject.activeInHierarchy)
            animator.SetInteger(PARAM_STATE, (int)state);

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
}
