using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    //This script will be added to every object using a stat
    // A scriptable object would be used to get the info of each character

    public float health;
    public float movementSpeed;
    public Action OnDeathEvent;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if(health <= 0)
            OnDeathEvent?.Invoke();
    }
}
