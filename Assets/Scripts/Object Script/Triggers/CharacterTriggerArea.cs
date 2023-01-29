using UnityEngine;
using static Units;

public class CharacterTriggerArea : MonoBehaviour
{
    [Header("For Hero characters/ Player Units")]
    [SerializeField] private Hero hero;

    [Header("For AI Units")]
    [SerializeField] private Enemy self;

    [Header("For Ranged Units")]
    [SerializeField] private bool isRanged;

    private void OnTriggerEnter(Collider other)
    {
        //For Hero
        if(other.gameObject.tag == "Enemy")
        {
            if (hero == null) return;
            Debug.Log("Enemy has entered area");
            hero.AddTarget(other.gameObject);
        }

        //For Enemies
        if (isRanged)
        {
            if(other.gameObject.tag == "Tower")
            {
                self.AddTarget(other.gameObject,UNIT_TYPE.TOWER);
            }
            else if (other.gameObject.tag == "TownHall")
            {
                Debug.Log("In town hall range");
                //self.gameObject.GetComponent<RangeEnemy>().SetRangedAttack(other.gameObject);
            }
            else if (other.gameObject.tag == "Hero")
            {
                self.AddTarget(other.gameObject, UNIT_TYPE.HERO);
            }
        }
        else
        {
            //Melee
            if (other.gameObject.tag == "Hero")
            {
                if (self != null)
                {
                    self.AddTarget(other.gameObject, UNIT_TYPE.HERO);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(hero !=  null)
                hero.RemoveTarget(other.gameObject);
        }

        if (other.gameObject.tag == "Hero")
        {
            if (self != null)
            {
                self.RemoveTarget(other.gameObject);
            }
        }

        if(other.gameObject.tag == "Tower")
        {
            if(self != null)
            {
                self.RemoveTarget(other.gameObject);
            }
        }
    }
}
