using System.Collections;
<<<<<<< Updated upstream
using System.Collections.Generic;
using UnityEngine;
=======
using System.Diagnostics;
>>>>>>> Stashed changes

public class BoxSpawnerNew : MonoBehaviour
{
    public GameObject boxPrefab;
<<<<<<< Updated upstream
    public float spawnInterval = 2f;
    public Vector3 spawnAreaSize = new Vector3(10f, 1f, 10f);

    void Start()
    {
        InvokeRepeating(nameof(SpawnBox), 0f, spawnInterval);
    }

    void SpawnBox()
=======
    public float spawnInterval = 10f;
    public Vector3 spawnAreaSize = new Vector3(25f, 25f, 25f);
    public bool spawn = true;
    private Stopwatch stopwatch;
    private float startTime;
    private float elapsedTime;
    
    

    void Start()
    {
        InvokeRepeating("SpawnMinionsHelperHelper", 1.5f, 12f);
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    void Update()
    {
        elapsedTime =  (float) stopwatch.Elapsed.TotalSeconds;
        radiusDyna(elapsedTime);
    }
    
    bool radiusDyna(float time)
    {
        if (time < 120)
        {
           spawn = true;
        } else if (time > 120 && time < 167)
        {
            spawn = false;
        } else if (time > 167 && time < 227)
        {
            spawn = true;
        } else if (time > 227)
        {
            spawn = false;
        }
        return true;
    }

    void SpawnMinionsHelperHelper()
    {
        if (spawn)
        {
            SpawnMinionsHelper();
        }
    }
    void SpawnMinionsHelper(int numMinions = 8, float spawnTime = 0.12f, int movementType = 0)
    {
        for (int i = 0; i < 12; i++) {
            StartCoroutine(spawnMinions(spawnTime, movementType));
        }
    }
    IEnumerator spawnMinions(float spawnTime=0.08f, int movementType=1)
    {
        SpawnBox(movementType);
        yield return new WaitForSeconds(spawnTime);
    }

    void SpawnBox(int movementCode=0)
>>>>>>> Stashed changes
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
