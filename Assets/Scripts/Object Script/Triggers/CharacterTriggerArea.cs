using UnityEngine;

public class CharacterTriggerArea : MonoBehaviour
{
    [Header("For Hero characters/ Player Units")]
    [SerializeField] private Hero hero;

    [Header("For AI Units")]
    [SerializeField] private Enemy self;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy has entered area");

            if (hero == null) return;
            hero.AddTarget(other.gameObject);
        }

        if(other.gameObject.tag == "Hero")
        {
            if(self != null)
            {
                self.SetTarget(other.gameObject);
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
