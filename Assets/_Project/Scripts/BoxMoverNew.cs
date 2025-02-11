using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public float speed = 2f;  // Adjust speed as needed
    private Transform player;

    void Start()
    {
        // Find the player in the scene by tag (make sure your player has the tag "Player")
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("hitbox1")) // Ensure the player has the "Player" tag
        {
            Debug.Log("Box touched the hitbox!");
            Destroy(gameObject); // Destroy the box
        }
    }
}