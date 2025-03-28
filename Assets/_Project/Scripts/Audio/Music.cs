using UnityEngine;

[CreateAssetMenu(fileName = "Music", menuName = "ScriptableObjects/MusicScriptableObject", order = 1)]
public class Music : ScriptableObject
{
    public string songName;
    public AudioClip clip;
    [Space(10)]
    [Header("Audio Clip Properties")]
    [Tooltip("Clip Properties. Must be set if this clip will be used as gameplay track")]
    public float bpm;
    public float offset;
}
