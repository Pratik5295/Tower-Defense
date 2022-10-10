
using UnityEngine;

public class SplashProjectile : Bullet
{
    protected override void HitTarget()
    {
        Collider[] allOverlappingColliders = Physics.OverlapBox(this.transform.position, new Vector3(1,0.5f,1));

        foreach(Collider collider in allOverlappingColliders)
        {
            if(collider.gameObject.tag == "Enemy")
            {
                Debug.Log("Hitting Enemy");
                CharacterStats enemyStats = collider.gameObject.GetComponent<CharacterStats>();
                enemyStats.TakeDamage(damage);
            }
        }
        Destroy(this.gameObject);
    }
}
