using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] int maxEnemies = 10;

    int currentEnemies = 0;

    void Start()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        if (currentEnemies < maxEnemies)
        {
            Vector3 spawnPos = transform.position + new Vector3(
                Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)
            );
            Instantiate(EnemyPrefab, spawnPos, Quaternion.identity);
            currentEnemies++;
        }
    }

    public void OnEnemyDied()
    {
        currentEnemies--;
        SpawnEnemy();
    }
}