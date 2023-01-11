using UnityEngine;
using static UITowerButton;
using static Units;

public class RangeEnemy : Enemy
{
    //The point from which the projectile/spear will be instantiated
    public GameObject throwPoint;

    //The prefab for projectile
    public GameObject throwProjectile;

    public override void SetTarget(GameObject _target)
    {
        

        //Check if the current target is already a tower, then dont switch targets

        //if (currentTarget.gameObject.tag == "Tower")
        //{
        //    AddTarget(_target, UNIT_TYPE.TOWER);

        //    var tower = currentTarget.GetComponent<Tower>();
        //    tower.OnTowerDestroyEvent += SearchForNewTarget;
        //    SetRangedAttack(_target);
        //}
        //else
        //{
        //    currentTarget = _target;
        //    AddTarget(_target, UNIT_TYPE.TOWNHALL);
        //}

        //if (currentTarget.GetComponent<Hero>() != null)
        //{
        //    hero = currentTarget.GetComponent<Hero>();
        //    hero.OnDeathEvent += SetTargetToHall;
        //    SetRangedAttack(_target);
        //}
        //else if(currentTarget.gameObject.tag == "TownHall")
        //{
        //    base.SetTarget(_target);
        //}
    }

    public void SetRangedAttack()
    {
        //Rotate facing

        targetLocation = this.transform.position;
        stats.SetState(CharacterStats.State.BATTLE);
        stats.SetTargetLocation(targetLocation);
    }


    public void Throw()
    {
        if (currentTarget == null) return;

        GameObject projectile = Instantiate(throwProjectile, throwPoint.transform.position, Quaternion.identity);

        projectile.GetComponent<ThrowProjectile>().SetTarget(currentTarget);
    }


    protected override void GetPriorityTarget()
    {
        //Priority for targets
        // 1 - Tower
        // 2 - Hero
        // 3 - Townhall

        if (potentialTargets.Count > 0)
        {
            currentTarget = potentialTargets[0];

            var tower = potentialTargets[0].GetComponent<Tower>();
            tower.OnTowerDestroyEvent += OnTowerDestroyedEventHandler;

            //Unsubscribe from hero dead event
            if (hero != null)
            {
                hero.OnDeathEvent -= SetTargetToHall;
            }
            return;
        }
        else if(hero != null)
        {
            currentTarget = hero.gameObject;

            if (currentTarget.GetComponent<Hero>() != null)
            {
                hero = currentTarget.GetComponent<Hero>();
                hero.OnDeathEvent += SetTargetToHall;
            }

            return;
        }
        else if (townHall != null)
        {
            currentTarget = townHall;
            return;
        }
    }

    private void OnTowerDestroyedEventHandler()
    {
        RemoveTarget(currentTarget);

        if (currentTarget != null)
        {
            SetTarget(currentTarget);
        }
    }
}
