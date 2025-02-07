using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawnerNew : MonoBehaviour
{
    public GameObject boxPrefab;
    public float spawnInterval = 2f;
    public Vector3 spawnAreaSize = new Vector3(25f, 25f, 25f);

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

        
        GameObject newBox = Instantiate(boxPrefab, randomPosition, Quaternion.identity);
        newBox.AddComponent<MoveTowardsPlayer>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
