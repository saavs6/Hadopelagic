using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 10f;
    public float spawnDelay = 0.12f;
    public int minionsPerSpawn = 8;
    public int spawnCount = 8;
    public Vector3 spawnAreaSize = new Vector3(25f, 25f, 25f);

    void Start()
    {
        InvokeRepeating(nameof(SpawnMinions), 1.5f, spawnInterval);
    }

    void SpawnMinions()
    {
        if (transform.childCount < spawnCount) {
            StartCoroutine(SpawnMinionWave());
        }
    }

    IEnumerator SpawnMinionWave()
    {
        for (int i = 0; i < minionsPerSpawn; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy(int movementCode = 0)
    {
        Vector3 randomPosition = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        Instantiate(enemyPrefab, randomPosition, Quaternion.identity, transform);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
