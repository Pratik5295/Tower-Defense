using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Turret : MonoBehaviour
{
    //This will be connected to the tower's turret
    public GameObject projectilePrefab;
    public GameObject barrelPoint;

    [SerializeField] private float shootingCounter;
    [SerializeField] private float maxShootingCounter;

    [SerializeField] private GameObject target;

    public float speed = 1.0f;

    private void Start()
    {
        shootingCounter = maxShootingCounter;
    }

    private void Update()
    {
        if (target != null)
        {
            TargetRotation();
            shootingCounter -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (shootingCounter <= 0 && target != null)
        {
            Debug.Log($"{gameObject.name} is shooting");
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
        target = enemy;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, barrelPoint.transform.position, barrelPoint.transform.rotation);

        bullet.GetComponent<Bullet>().SetTarget(target);
    }

}
