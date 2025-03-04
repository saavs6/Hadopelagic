using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MovePattern
{
    void Move(GameObject box, Transform player, float speed);
}

public class MoveStraight : MovePattern
{
    public void Move(GameObject box, Transform player, float speed)
    {
        Vector3 direction = (player.position - box.transform.position).normalized;
        box.transform.position += direction * speed * Time.deltaTime;
    }
}

public class MoveCircle : MovePattern
{
    private float angle = 0f;
    private Vector3 centerPoint;
    private float radius=5;

    public MoveCircle(Vector3 centerPoint)
    {
        this.centerPoint = centerPoint;
    }

    public MoveCircle(Vector3 centerPoint, float radius)
    {
        this.centerPoint = centerPoint;
        this.radius = radius;
    }

    public void Move(GameObject box, Transform player, float speed)
    {
        angle += speed * Time.deltaTime;
        Vector3 circlePos = centerPoint + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
        box.transform.position = Vector3.MoveTowards(box.transform.position, circlePos, speed * Time.deltaTime);
        box.transform.position = Vector3.MoveTowards(box.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}

public class MoveTowardsPlayer : MonoBehaviour
{
    public float speed = 4f;  // Adjust speed as needed

    private Transform player;

    public MovePattern movementPattern;

    public Vector3 worldCenter = Vector3.zero; //for the enemies that move in circles

    public Vector3 spawnAreaSize = new Vector3(0.26f, 0.26f, 0.26f);

    private bool stopped = false;

    public GameObject spherePrefab;

    void Start()
    {
        // Find the player in the scene by tag (make sure your player has the tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;

        int pattern = Random.Range(0, 2); // adjust range if you want to add more patterns
        movementPattern = GetMovementPattern(pattern);
        spherePrefab = GameObject.Find("Sphere");
    }

    void Update()
    {
        if (player != null && movementPattern != null && !stopped) 
        {
            movementPattern.Move(gameObject, player, speed);
        } else {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.freezeRotation = true;
        }
    }

    public MovePattern GetMovementPattern(int pattern)
    {
        switch (pattern)
        {
            case 0:
                return new MoveStraight();
            case 1:
                Vector3 randomOffset = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                Vector3 orbitCenter = (player.transform.position + gameObject.transform.position)/2;

                float radius = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - gameObject.transform.position.x, 2)+
                Mathf.Pow(player.transform.position.y - gameObject.transform.position.y, 2)+
                Mathf.Pow(player.transform.position.z - gameObject.transform.position.z, 2))/2;

                return new MoveCircle(orbitCenter, radius);
            default:
                return new MoveStraight();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hitbox1")) // Ensure the player has the "Player" tag
        {
            stopped = true;
            /*
            GameObject sphere1 = SpawnParrySphere();
            GameObject sphere2 = SpawnParrySphere();
            GameObject sphere3 = SpawnParrySphere();
            */
        }
    }
    GameObject SpawnParrySphere()
    {
        Vector3 sPos = new Vector3(
            (transform.position.x + player.position.x)/2 + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            (transform.position.y + player.position.y)/2 + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            (transform.position.z + player.position.z)/2 + Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        GameObject parrySphere = Instantiate(spherePrefab, sPos, Quaternion.identity);
        parrySphere.AddComponent<StopNow>();
        return parrySphere;
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public float speed = 4f;  // Adjust speed as needed
    private Transform player;
    private bool stopped = false;
    public GameObject spherePrefab;
    public float spawnInterval = 2.5f;
    public Vector3 spawnAreaSize = new Vector3(0.26f, 0.26f, 0.26f);

    void Start()
    {
        // Find the player in the scene by tag (make sure your player has the tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spherePrefab = GameObject.Find("Sphere");
    }

    void Update()
    {
        if (player != null && !stopped)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        } else {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hitbox1")) // Ensure the player has the "Player" tag
        {
            stopped = true;
            GameObject sphere1 = SpawnParrySphere();
            GameObject sphere2 = SpawnParrySphere();
            GameObject sphere3 = SpawnParrySphere();
        }
    }
    GameObject SpawnParrySphere(){
        
        Vector3 sPos = new Vector3(
            (transform.position.x + player.position.x)/2 + Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            (transform.position.y + player.position.y)/2 + Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            (transform.position.z + player.position.z)/2 + Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );
        GameObject parrySphere = Instantiate(spherePrefab, sPos, Quaternion.identity);
        parrySphere.AddComponent<StopNow>();
        return parrySphere;
    }
}
*/