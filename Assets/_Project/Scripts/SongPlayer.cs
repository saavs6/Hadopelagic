
using UnityEngine;

public class SongPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip song;
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(song);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
