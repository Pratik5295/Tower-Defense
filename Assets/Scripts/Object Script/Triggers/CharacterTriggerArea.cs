using UnityEngine;

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
        if(other.gameObject.tag == "Enemy")
        {
            if (hero == null) return;
            Debug.Log("Enemy has entered area");
            hero.AddTarget(other.gameObject);
        }

        if(other.gameObject.tag == "Hero")
        {
            if(self != null)
            {
                self.SetTarget(other.gameObject);
            }
        }

        if (isRanged)
        {
            if(other.gameObject.tag == "Tower")
            {

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            hero.RemoveTarget(other.gameObject);
        }

        if (other.gameObject.tag == "Hero")
        {
            if (self != null)
            {
                self.SetTarget(null);
            }
        }
    }
}
