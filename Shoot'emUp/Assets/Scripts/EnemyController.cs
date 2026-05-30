using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject hitPrefab;
    [SerializeField] GameObject explosionPrefab;
    const float DESTROY_HEIGHT = -6f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y < DESTROY_HEIGHT)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DestroyEnemy();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DestroyEnemy();
    }

   void DestroyEnemy()
{
    if(GameManager.GetInstance() == null){
        Debug.Log("GameManager es NULL");
        return;
    }
    GameManager.GetInstance().AddScore(100);
    Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    Destroy(gameObject);
}
}