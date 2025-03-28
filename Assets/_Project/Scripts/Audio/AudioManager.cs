using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    [SerializeField] private Music[] music;
    [SerializeField] private Sound[] sound;
    private Dictionary<string, AudioClip> musicDict;
    private Dictionary<string, AudioClip> soundDict;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource; //We may want to pool audio sources or switch to a different method
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Event needed from another game. May be useful to reset combos or do things between songs
    //public static Action<string> OnSongChanged;

    private float timeToPlayMusic = 1f;
    private void Awake()
    {
        Instance = this;
        musicDict = new Dictionary<string, AudioClip>();
        soundDict = new Dictionary<string, AudioClip>();
        if (musicSource == null) Debug.LogError("Music source in AudioManager is null");
        foreach (Music m in music) musicDict.Add(m.name, m.clip);
        foreach (Sound s in sound) soundDict.Add(s.name, s.clip);
    }

    private void Start()
    {
        //Play a random song to test and pass dspTime info to the Conductor
        var index = Random.Range(0, music.Length);
        musicSource.clip = music[index].clip;
        PlaySong(0);
    }

    private void PlaySong(int index)
    {
        //We may want to change when we schedule the audio, but for now we give a one second delay via timeToPlayMusic
        musicSource.clip = music[index].clip;
        musicSource.PlayScheduled(AudioSettings.dspTime + timeToPlayMusic);
        Conductor.Instance.SetSongParameters(music[index], AudioSettings.dspTime + timeToPlayMusic);
    }
    
    /// <summary>
    /// Play a sound effect. Chen: This jit ain't tested at all lmao
    /// Theoretically PlayOnShot can play multiple sounds without interrupting another one?
    /// May want to introduce pitch variation. We can probably get away with "late" sound effects
    /// </summary>
    public void PlaySound(string soundName)
    {
        if (soundDict.TryGetValue(soundName, out var clip))
        {
            soundSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Song " + soundName + " not found!");
        }
    }
}
