using UnityEngine;

public class ShootController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other) {
    Destroy(gameObject);
}
}