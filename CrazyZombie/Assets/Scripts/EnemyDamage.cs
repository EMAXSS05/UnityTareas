using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    const int HITS_TO_DIE = 3;
    int hitCount = 0;

    EnemySpawner spawner;

    void Start()
    {
        spawner = FindObjectOfType<EnemySpawner>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            hitCount++;
            Destroy(other.transform.root.gameObject);

            if (hitCount >= HITS_TO_DIE)
            {
                if (spawner != null)
                {
                    spawner.OnEnemyDied(); 
                    spawner.SpawnEnemy();  
                }
                Destroy(gameObject);
            }
        }
    }
}