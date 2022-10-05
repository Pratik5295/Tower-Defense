using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    [SerializeField] private Turret turret;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy has entered area");
            turret.SetTarget(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy has left area");
            turret.SetTarget(null);
        }
    }
}
