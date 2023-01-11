using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] private GameObject target;
    [SerializeField] private Rigidbody rb;
    public float speed;

    public void SetTarget(GameObject obj)
    {
        target = obj;
    }


    private void Update()
    {
        Shooting();
       ProjectileRotation();
    }

    public float GetDamage()
    {
        return damage;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    private void Shooting()
    {
        if(target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector3 dir = target.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    private void ProjectileShooting()
    {
        ProjectileMotion pm = new ProjectileMotion();
        pm.AssignValuesForPhysics(this.gameObject, target, rb,-18f);
    }

    private void ProjectileRotation()
    {
        if (target == null) return;
        float singleStep = 3f * Time.deltaTime;
        Vector3 targetDirection = target.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    protected virtual void HitTarget()
    {
        if (target.tag == "Enemy")
        {
            CharacterStats enemyStats = target.GetComponent<CharacterStats>();
            enemyStats.TakeDamage(damage);
        }
        Destroy(this.gameObject);
    }
}
