using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LauncherPM : MonoBehaviour
{
    public GameObject projectile;
    public GameObject target;
    public Rigidbody rb;

    private void Start()
    {
        Shoot();
    }
    private void Shoot()
    {
        ProjectileMotion pm = new ProjectileMotion();
        pm.AssignValuesForPhysics(projectile, target, rb, -18f);
    }
}
