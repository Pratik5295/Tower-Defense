using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject target;

    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
    }

    public void Movement()
    {
        Vector3 targetPosition = target.transform.position;

        Vector3 directionVector = target.transform.position - this.transform.position;

        rb.velocity = directionVector;
    }
}
