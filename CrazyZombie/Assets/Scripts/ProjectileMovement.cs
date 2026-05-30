using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] float speed = 20f;

    Rigidbody rb;
    Vector3 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        rb.linearVelocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider collision)
{
    Destroy(gameObject);
}
}