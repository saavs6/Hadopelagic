using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using System.Diagnostics;
public class SwarmController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyPrefab; // Reference to enemy prefab
    public int enemyCount = 200; // Number of enemies to spawn
    public float spawnRadius = 5f; // Distance from player to spawn
    public ArrayList NumberArray = new ArrayList();
    public int AttackerCount = 8;
    private bool triggered = false;
    
    private Stopwatch stopwatch;
    private float startTime;
    private float elapsedTime;

    public List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        SpawnEnemies(); // Call function to spawn enemies
        StartCoroutine(AttackRoutine()); // Start attack behavior
        for (int i = 2; i < 50; i += 2)
        {
            NumberArray.Add(i);
        }
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    void Update()
    {
        elapsedTime = (float) stopwatch.Elapsed.TotalSeconds;
        if (!triggered && elapsedTime > 200)
        {
            triggered = true;
            SpawnEnemies(true);
            SpawnEnemies(true);
            SpawnEnemies(true);
            SpawnEnemies(true);
        }

        radiusDyna(elapsedTime);
    }
    
    bool radiusDyna(float time)
    {
        if (time < 120)
        {
            AttackerCount = 0;
        } else if (time > 120 && time < 167)
        {
            AttackerCount = 10;
        } else if (time > 167 && time < 227)
        {
            AttackerCount = 0;
        } else if (time > 227)
        {
            AttackerCount = 35;
        }
        else
        {
            AttackerCount = 0;
        }

        return true;
    }

    void SpawnEnemies(bool two=false)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = GetSpawnPosition();
            GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            // Ensure enemy has EnemyAI script and assign the player
            EnemyAI enemyAI = newEnemy.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.player = player.transform;
            }

            if (two)
            {
                enemyAI.wave2 = true;
            }

            enemies.Add(newEnemy); // Add to list
        }
    }

    Vector3 GetSpawnPosition()
    {
        // Generate a random position in a circle around the player
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float x = Mathf.Cos(angle) * spawnRadius;
        float z = Mathf.Sin(angle) * spawnRadius;
        Vector3 spawnPosition = new Vector3(player.transform.position.x + x + Random.Range(-1f,1.5f), player.transform.position.y+Random.Range(1f, 3f), player.transform.position.z + z + Random.Range(-1f,1.5f));
        return spawnPosition;
    }

    IEnumerator AttackRoutine()
    {
        while (true) {
            yield return new WaitForSeconds(4f);

            if (enemies.Count > 0)
            {
                for (int i = 0; i < AttackerCount; i++)
                {
                    yield return new WaitForSeconds(0.12f);
                    GameObject attacker = enemies[Random.Range(0, enemies.Count)];
                    if (attacker != null)
                    {
                        attacker.GetComponent<EnemyAI>().AttackPlayer();
                    }
                }
            }
        }
    }
}