using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    //This script will be responsible for the shooting only
    //This will be connected to the tower's turret

    public GameObject projectilePrefab;
    public GameObject barrelPoint;

    [SerializeField] private float shootingCounter;
    [SerializeField] private float maxShootingCounter;

    [SerializeField] private GameObject target;

    [SerializeField] private List<GameObject> potentialTargets;

    public float speed = 1.0f;

    private void Start()
    {
        shootingCounter = maxShootingCounter;
        potentialTargets = new List<GameObject>();
    }

    private void Update()
    {
        if (target != null)
        {
            TargetRotation();
            shootingCounter -= Time.deltaTime;
        }
        else
        {
            if (potentialTargets.Count > 0)
            {
                //Set target as our current target got away or died
                Debug.Log("Searching for new target");
                if (potentialTargets[0] == null)
                {
                    potentialTargets.RemoveAt(0);
                }
                else
                {
                    SetTarget(potentialTargets[0]);
                }
            }
        }
    }

    private void LateUpdate()
    {
        if (shootingCounter <= 0 && target != null)
        {
            Shoot();
            shootingCounter = maxShootingCounter;
        }
    }

    private void TargetRotation()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = target.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }


    //Shooting logic
    public void SetTarget(GameObject enemy)
    {
        if (target != null) return;
        if (enemy == null) return;
        target = enemy;

        Enemy enem = target.GetComponent<Enemy>();
        enem.OnEnemyDeathEvent += OnEnemyDeathEventListener;
    }

    private void OnEnemyDeathEventListener()
    {
        if (target == null) return;
        Debug.Log($"{target.gameObject.name} has died");
        Enemy enem = target.GetComponent<Enemy>();
        int killBounty = enem.bounty;
        enem.OnEnemyDeathEvent -= OnEnemyDeathEventListener;
        RemoveFromTargetList(target);
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public void AddToTargetList(GameObject enemy)
    {
        if (potentialTargets.Contains(enemy)) return;

        potentialTargets.Add(enemy);
    }
    public void RemoveFromTargetList(GameObject enemy)
    {
        if (!potentialTargets.Contains(enemy)) return;

        potentialTargets.Remove(enemy);
        Enemy enem = target.GetComponent<Enemy>();
        enem.OnEnemyDeathEvent -= OnEnemyDeathEventListener;
        target = null;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, barrelPoint.transform.position, barrelPoint.transform.rotation);

        bullet.GetComponent<Projectile>().SetTarget(target);
    }

}
