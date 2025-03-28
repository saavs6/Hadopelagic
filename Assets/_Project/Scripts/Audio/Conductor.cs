using System;
using Oculus.Interaction.DebugTree;
using Unity.Mathematics;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.Serialization;

public class Conductor : MonoBehaviour
{
    public static Conductor Instance { get; private set; }
    
    public float bpm;
    [Tooltip("Length of a single beat in the song")]
    public float beat;
    [Tooltip("When the song started playing relative to DSPTime")]
    public double startTime;

    [Tooltip("Current position of the song in ms")]
    public float songPosition;

    [Tooltip("Small gap at the beginning of every MP3 used for meta-data")]
    public float offset;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        /*
        Taken from https://fizzd.me/posts/how-to-make-a-rhythm-game-a-quick-and-dirty-guide-to-setting-up-your-project
        We'll use songPosition to query against the beatmap. Thus this should be updated every frame
        */
        songPosition = (float) (AudioSettings.dspTime - startTime) - offset;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DistanceFromBeat();
        }
    }

    public void SetSongParameters(Music music, double dspStartTime)
    {
        bpm = music.bpm;
        beat = 60 / bpm;
        offset = music.offset;
        startTime = (float) dspStartTime;
    }
    
    public float DistanceFromBeat()
    {
        Debug.Log($"Distance from beat: {Mathf.Min((songPosition % beat), beat - (songPosition % beat))}");
        return Mathf.Min((songPosition % beat), beat - (songPosition % beat));
    }
}
