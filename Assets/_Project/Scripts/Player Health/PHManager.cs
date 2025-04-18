using System.Collections;
using UnityEngine;

public class PHManager : MonoBehaviour
{
    public int maxHealth = 8;
    public int currentHealth;
    private bool regening = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        if (damage >= currentHealth)
        {
            currentHealth = 0;
            LevelManager.removeShield(currentHealth);
        }
        else
        {
            currentHealth -= damage;
            LevelManager.removeShield(damage);
        }
        if (regening)
        {
            StopCoroutine(Regen());
        }
        StartCoroutine(Regen());
    }

    public IEnumerator Regen()
    {
        regening = true;
        yield return new WaitForSeconds(10);
        regening = false;
        RestoreHealth();
    }
    
    public void RestoreHealth()
    {
        currentHealth = 8;
        LevelManager.addShield(1);
    }

    public void Heal()
    {
        if (currentHealth != 8)
        {
            int newHealth = (currentHealth + 1) % 8;
            LevelManager.addShield(newHealth - currentHealth);
            currentHealth = newHealth;
        }
        
    }
}
