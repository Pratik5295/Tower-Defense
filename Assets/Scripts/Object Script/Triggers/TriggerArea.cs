using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    [SerializeField] private Turret turret;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy has entered area");
            turret.AddToTargetList(other.gameObject);
            turret.SetTarget(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && turret.GetTarget() != null)
        {
            turret.RemoveFromTargetList(other.gameObject);
        }
    }
}
