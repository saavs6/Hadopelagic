using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawnerNew : MonoBehaviour
{
    public GameObject boxPrefab;
    public float spawnInterval = 2f;
    public Vector3 spawnAreaSize = new Vector3(10f, 1f, 10f);

    void Start()
    {
        InvokeRepeating(nameof(SpawnBox), 0f, spawnInterval);
    }

    void SpawnBox()
    {
        Vector3 randomPosition = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        Instantiate(boxPrefab, randomPosition, Quaternion.identity);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
