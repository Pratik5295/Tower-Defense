using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Hero character;

    public void AttackAnimator()
    {
        character.Battle();
    }
}
