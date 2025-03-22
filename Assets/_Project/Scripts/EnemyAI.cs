using System.Collections;
using Oculus.Interaction;
using Unity.Hierarchy;
using UnityEngine;
using System.Diagnostics;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float attackSpeed = 4f;
    private bool isAttacking = false;
    public bool stopped = false;
    private Stopwatch stopwatch;
    private float startTime;
    private float elapsedTime;
    private bool triggered = false;
    private bool triggered2 = false;
    
    public float orbitSpeed = 0.005f; // Speed of circling
    public float orbitRadius = 5f; // Distance from player
    public float attackStopDistance = 1.5f; // Stop distance before attacking
    private float orbitAngle;
    private Vector3 orbitCenterOffset; // Unique offset for each enemy
    private float height;
    public bool wave2 = false;
    
    public Color glowColor = Color.red; // Red glow color
    public float glowIntensity = 2f; // Intensity of the glow
    private Renderer enemyRenderer;
    private Material enemyMaterial;

    void Start()
    {
        if (player != null)
        {
            orbitCenterOffset = transform.position - player.position; // Preserve unique start offset
            orbitRadius = orbitCenterOffset.magnitude; // Ensure the radius stays the same
            orbitAngle = Mathf.Atan2(orbitCenterOffset.z, orbitCenterOffset.x); // Get initial angle
        }

        height = Random.Range(0f, 4.5f);
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }
    
    void Update()
    {
        elapsedTime = (float) stopwatch.Elapsed.TotalSeconds;
        if (!isAttacking)
        {
            OrbitAroundPlayer();
            if (!wave2)
            {
                radiusDyna(elapsedTime);
            } else if (wave2)
            {
                radiusDyna2(elapsedTime);
            }
        }
    }

    IEnumerator DangerIndicator()
    {
        enemyRenderer = GetComponent<Renderer>();
        enemyMaterial = enemyRenderer.material;
        yield return new WaitForSeconds(2f);
        // Enable emission
        enemyMaterial.EnableKeyword("_EMISSION");
        enemyMaterial.SetColor("_EmissionColor", glowColor * glowIntensity);
        yield return new WaitForSeconds(1f);
        enemyMaterial.DisableKeyword("_EMISSION");
        triggered2 = false;
    }

    bool radiusDyna(float time)
    {
        if (time < 120)
        {
            orbitRadius = 100;
        } else if (time > 120 && time < 167)
        {
            orbitRadius = 7;
        } else if (time > 167 && time < 227)
        {
            orbitRadius = 100;
        } else if (time > 227)
        {
            orbitRadius = 9;
        }
        return true;
    }
    
    bool radiusDyna2(float time)
    {
        if (time < 27)
        {
            orbitRadius = 100;
        } else if (time > 27)
        {
            orbitRadius = 7;
        }
        return true;
    }
    
    void OrbitAroundPlayer()
    {
        if (player == null) return;

        orbitAngle += orbitSpeed * Time.deltaTime; // Keep moving in a circular motion

        float x = Mathf.Cos(orbitAngle) * orbitRadius;
        float z = Mathf.Sin(orbitAngle) * orbitRadius;
        Vector3 orbitPosition = player.position + new Vector3(x, height, z);

        transform.position = orbitPosition;
        transform.LookAt(player); // Face the player while orbiting
        transform.Rotate(0,180,0);
    }

    public void AttackPlayer()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            StartCoroutine(MoveToPlayerAndAttack());
        }
    }

    IEnumerator MoveToPlayerAndAttack()
    {
        while (!stopped)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, attackSpeed * Time.deltaTime);
            transform.LookAt(player.position);
            transform.Rotate(0,90,0);
            yield return null;
        }

        yield return new WaitForSeconds(4f); // Wait before returning to swarm
        
        isAttacking = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hitbox1")) // Ensure the player has the "Player" tag
        {
            stopped = true;
            DangerIndicator();
            /*
            GameObject sphere1 = SpawnParrySphere();
            GameObject sphere2 = SpawnParrySphere();
            GameObject sphere3 = SpawnParrySphere();
            */
        }
    }

    void OnTriggerExit(Collider other)
    {
        stopped = false;
    }
}