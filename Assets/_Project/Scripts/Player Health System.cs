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
    private float attackTime;
    public ConsoleEdit output;
    public bool alive = true;
    
    
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
       if ((other.CompareTag("Enemy") || other.CompareTag("Boss")) && !enemyDamageCoroutines.ContainsKey(other) && alive) // Check if the object is an enemy
        {
            //TODO implement once enemy script is done
            //boss tags assume colliders will have boss tags.
            if (other.CompareTag("Boss"))
            {
                foreach (Collider col in enemyDamageCoroutines.Keys) {
                    if (col.CompareTag("Boss")) {return;}   //leave if 
                }
            }

            //if enemy, get enemy script component's dmg
            //if boss, get boss script component's dmg
            int damage = 1; 

            Coroutine newCoroutine = StartCoroutine(DamageOverTime(other, damage));
            enemyDamageCoroutines.Add(other, newCoroutine);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss")) //TODO
        {
            if (enemyDamageCoroutines.ContainsKey(other))
            {
                StopCoroutine(enemyDamageCoroutines[other]); // Stop the specific enemy's coroutine
                enemyDamageCoroutines.Remove(other);
            }
        }
    }

    IEnumerator DamageOverTime(Collider enemy, int damage = 1)
    {
        yield return new WaitForSeconds(attackTime); // Apply damage every second
        if (enemy != null && enemyDamageCoroutines.ContainsKey(enemy) && alive && !immune) // Check if enemy still exists
        {
            currentHP -= damage; 
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
                //play shield break sound
            }
        } else if (immune)
        {
            //play block sound
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
            //play regen sound
        }
    }

    void Die()
    {
        alive = false;
        enemyDamageCoroutines.Clear();
        //implement further logic
        //lmfao skull emoji ekull emoji ggez krill issue
    }
}
