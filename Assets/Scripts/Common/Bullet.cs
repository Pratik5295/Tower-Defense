using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject target;
    public float speed;

    public void SetTarget(GameObject obj)
    {
        target = obj;
    }


    private void Update()
    {
        Shooting();
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

    private void HitTarget()
    {
        Debug.Log("Hit target!");
        Destroy(this.gameObject);
    }
}
