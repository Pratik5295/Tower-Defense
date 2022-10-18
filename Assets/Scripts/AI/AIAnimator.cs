using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimator : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    public void AttackAnimator()
    {
        enemy.Battle();
    }
}
