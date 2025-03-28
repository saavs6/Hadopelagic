using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// Visualizes the effects of the music beat
/// </summary>
public class MusicPulse : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    private float lastBeat;

    private Coroutine flashCoroutine;
    private float beat;

    private void Awake()
    {
        if (lights.Length == 0)
        {
            Debug.LogError("No lights assigned to MusicPulse");
        }
    }

    void Start()
    {
        //Initializing lastbeat to be 0 means the light doesn't pulse on the first beat
        lastBeat = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Conductor.Instance.songPosition > lastBeat + Conductor.Instance.beat)
        {
            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
            }
            flashCoroutine = StartCoroutine(Flash(Conductor.Instance.beat));
            lastBeat += Conductor.Instance.beat;
        }
    }

    private IEnumerator Flash(float flashTime)
    {
        while (flashTime > 0)
        {
            foreach (Light l in lights)
            {
                l.intensity = flashTime / Conductor.Instance.beat * 3f;
            }
            flashTime -= Time.deltaTime;
            yield return null;
        }
    }
}
