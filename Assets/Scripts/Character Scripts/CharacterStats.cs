using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    //This script will be added to every object using a stat
    // A scriptable object would be used to get the info of each character

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
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.value = health;

        if (health <= 0)
            OnDeathEvent?.Invoke();
    }
}
