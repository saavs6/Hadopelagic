using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrySpawnerNew : MonoBehaviour
{
    public GameObject spherePrefab;
    public float spawnInterval = 2f;
    public Vector3 spawnAreaSize = new Vector3(3f, 3f, 3f);

    /*
    void Start()
    {
        InvokeRepeating(nameof(SpawnSphere), 0f, spawnInterval);
    }
    */

    void SpawnSphere()
    {
        Vector3 randomPosition = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        Instantiate(spherePrefab, randomPosition, Quaternion.identity);
        randomPosition = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        Instantiate(spherePrefab, randomPosition, Quaternion.identity);
        randomPosition = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        Instantiate(spherePrefab, randomPosition, Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
