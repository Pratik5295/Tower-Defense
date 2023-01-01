using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBlastTrigger : MonoBehaviour
{
    public LayerMask m_LayerMask;
    public void AreaEffect()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);

        foreach(Collider collider in hitColliders)
        {
            Debug.Log($"Collided with: {collider.gameObject.name}");
            collider.gameObject.GetComponent<Enemy>().TakeDamage(5f);
        }
    }
}
