using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PHManager : MonoBehaviour
{
    private bool regening = false;
    private bool unShielded = false;
    
    public AudioSource audioSource;
    public AudioClip healSound;
    public AudioClip damageSound;
    public AudioClip shieldSound;
    public AudioClip dangerSound;
    
    public bool areYouShore = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TakeDamage(4);
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        
        if (LevelManager.Instance.shieldHitPoints == 0) {
            audioSource.PlayOneShot(shieldSound);
            SceneManager.LoadScene("Game Over");
        }
        
            if (damage >= LevelManager.Instance.shieldHitPoints)
            {
                if (!areYouShore)
                {
                    LevelManager.removeShield(LevelManager.Instance.shieldHitPoints);
                }

                audioSource.PlayOneShot(shieldSound);
                audioSource.PlayOneShot(dangerSound);
            }
            else
            {
                if (!areYouShore)
                {
                    LevelManager.removeShield(damage);
                }

                audioSource.PlayOneShot(damageSound);
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
        LevelManager.addShield(8);
        audioSource.PlayOneShot(healSound);
        unShielded = false;
    }

    public void Heal()
    {
        if (LevelManager.Instance.shieldHitPoints < LevelManager.Instance.maxShieldHitPoints)
        {
            LevelManager.addShield(1);
        }
        
    }
}
