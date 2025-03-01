using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawnerNew : MonoBehaviour
{
    public GameObject boxPrefab;
    public float spawnInterval = 10f;
    public Vector3 spawnAreaSize = new Vector3(25f, 25f, 25f);

    void Start()
    {
        InvokeRepeating("SpawnMinionsHelperHelper", 1.5f, 8f);
    }

    void SpawnMinionsHelperHelper()
    {
        SpawnMinionsHelper();
    }
    void SpawnMinionsHelper(int numMinions = 8, float spawnTime = 0.12f, int movementType = 0)
    {
        for (int i = 0; i < 10; i++) {
            StartCoroutine(spawnMinions(spawnTime, movementType));
        }
    }
    IEnumerator spawnMinions(float spawnTime=0.08f, int movementType=1)
    {
        SpawnBox(movementType);
        yield return new WaitForSeconds(spawnTime);
    }

    void SpawnBox(int movementCode=0)
    {
        Vector3 randomPosition = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        
        GameObject newBox = Instantiate(boxPrefab, randomPosition, Quaternion.identity);
        MoveTowardsPlayer moveScript = newBox.AddComponent<MoveTowardsPlayer>();
        moveScript.movementPattern = moveScript.GetMovementPattern(movementCode);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
