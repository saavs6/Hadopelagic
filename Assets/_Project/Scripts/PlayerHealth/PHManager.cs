using System.Collections;
using UnityEngine;

public class PHManager : MonoBehaviour
{
    public int maxHealth = 8;
    public int currentHealth;
    private bool regening = false;
    private bool unShielded = false;
    
    public AudioSource audioSource;
    public AudioClip healSound;
    public AudioClip shieldSound;
    public AudioClip dangerSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        TakeDamage(4);
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        if (damage >= currentHealth)
        {
            LevelManager.removeShield(currentHealth);
            currentHealth = 0;
            unShielded = true;
            audioSource.PlayOneShot(shieldSound);
            audioSource.PlayOneShot(dangerSound);
            
        }
        else
        {
            currentHealth -= damage;
            LevelManager.removeShield(damage);
        }
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
        LevelManager.addShield(8);
        audioSource.PlayOneShot(healSound);
        unShielded = false;
    }

    public void Heal()
    {
        if (currentHealth < maxHealth)
        {
            LevelManager.addShield(1);
            currentHealth++;
        }
        
    }
}
