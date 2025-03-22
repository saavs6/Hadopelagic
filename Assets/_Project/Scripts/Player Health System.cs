using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlayerHealthSystem : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int maxHP = 2;
    public int currentHP;
    public float regenTime = 10f;
    private Dictionary<Collider, Coroutine> enemyDamageCoroutines = new Dictionary<Collider, Coroutine>();
    public bool immune = false;
    private Coroutine immuneCoroutine;
    private Coroutine regenCoroutine;
    public float attackTime;
    public ConsoleEdit output;
    public bool alive = true;
    public AudioSource audioSource;
    public AudioClip breakSound; // Assign your sound in the Inspector
    public AudioClip blockSound;
    public AudioClip regenSound;
    public AudioClip deathSound;
    public AudioClip deathSound2;
    
    void Start()
    {
        currentHP = maxHP;
        attackTime = 3f;
    }

    void Update()
    {
        output.UpdateText("HP: " + currentHP);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !enemyDamageCoroutines.ContainsKey(other) && alive) // Check if the object is an enemy
        {
            Coroutine newCoroutine = StartCoroutine(DamageOverTime(other));
            enemyDamageCoroutines.Add(other, newCoroutine);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")) 
        {
            if (enemyDamageCoroutines.ContainsKey(other))
            {
                StopCoroutine(enemyDamageCoroutines[other]); // Stop the specific enemy's coroutine
                enemyDamageCoroutines.Remove(other);
            }
        }
    }

    IEnumerator DamageOverTime(Collider enemy)
    {
        yield return new WaitForSeconds(attackTime); // Apply damage every second
        if (enemy != null && enemyDamageCoroutines.ContainsKey(enemy) && alive && !immune) // Check if enemy still exists
        {
            currentHP--; 
            Debug.Log("Player took damage! Health: " + currentHP);
            if (currentHP > 0)
            {
                immuneCoroutine = StartCoroutine(immunityTime());
                regenCoroutine = StartCoroutine(regeneration());
            }
            if (currentHP <= 0)
            {
                Die();
            }
            else if (currentHP == 1)
            {
                audioSource.PlayOneShot(breakSound);
            }
        } else if (immune)
        {
            if (audioSource != null && blockSound != null)
            {
                audioSource.PlayOneShot(blockSound); // Plays the sound once
            }
        }
        enemyDamageCoroutines.Remove(enemy); // Remove from tracking after attack
    }

    IEnumerator immunityTime()
    {
        immune = true;
        yield return new WaitForSeconds(2f);
        immune = false;
    }

    IEnumerator regeneration()
    {
        yield return new WaitForSeconds(regenTime);
        if (currentHP == 1)
        {
            currentHP++;
            audioSource.PlayOneShot(regenSound);
        }
    }

    void Die()
    {
        alive = false;
        enemyDamageCoroutines.Clear();
        audioSource.PlayOneShot(deathSound);
        audioSource.PlayOneShot(deathSound2);
        //implement further logic
        //lmfao skull emoji ekull emoji ggez krill issue
    }
}
