using UnityEditorInternal;
using UnityEngine;

public class ProjectileMotion
{
    ///<summary>
    ///
    /// What we do need to calculate is the velocity
    /// The projectile will be a rigidbody 
    /// </summary>
    /// 

    public GameObject projectile, target;
    [SerializeField] private float distance;
    public Vector3 projectilePosition,targetPosition;

    [SerializeField] private float g;
    [SerializeField] private float h;

    private Rigidbody rb;


    public void AssignValuesForPhysics(GameObject _projectile, GameObject _target, Rigidbody _rb,float gravity)
    {
        projectile = _projectile;
        target = _target;
        projectilePosition = projectile.transform.position;
        targetPosition = target.transform.position;

        rb = _rb;
        g = gravity;

        if (projectilePosition.y > targetPosition.y) h = projectilePosition.y;
        else h = targetPosition.y;

        Physics.gravity = Vector3.up * g;
        rb.velocity = CalculateLaunchVelocity();

        Debug.Log($"Velocity: {rb.velocity}");
    }

    private Vector3 CalculateLaunchVelocity()
    {
        float displacementY = targetPosition.y - projectilePosition.y;
        Vector3 displacementXZ = new Vector3(targetPosition.x - projectilePosition.x, 0, targetPosition.z - projectilePosition.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * g * h);

        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * h / g) + Mathf.Sqrt(2 * (displacementY - h) / g));

        return velocityXZ + velocityY;
    }

}
