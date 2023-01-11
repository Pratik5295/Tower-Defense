using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : Projectile
{
    protected override void HitTarget()
    {
        GameObject tar = GetTarget();

        if(tar.tag == "Tower")
        {
            tar.GetComponent<Tower>().TakeDamage(damage);
        }
        else if(tar.tag == "Hero")
        {
            tar.GetComponent<CharacterStats>().TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
