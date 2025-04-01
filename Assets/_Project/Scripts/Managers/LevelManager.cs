using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public float[] waveOneDistances;

    private float startTime;
    private float songTime;
    private Dictionary<float, Level.SongAction> currentEventsMap;

    private int level = -1;
    private AudioSource musicPlayer;

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Instance.musicPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (currentEventsMap != null && currentEventsMap.TryGetValue(Instance.songTime, out var action))
        {
            Instance.songTime += Time.deltaTime;
            action?.Invoke();
        }
    }

    public static float GetElapsedTime()
    {
        if (Instance == null) {
            return 0f;
        }
        return Time.time - Instance.startTime;
    }

    private static int GetWaveOneMinionDistanceIndex(float elapsedTime)
    {
        if (elapsedTime < 15) {
            return 1;
        } else if (elapsedTime < 30) {
            return 0;
        } else {
            return 2;
        }
    }

    public static float GetMinionWaveOneOrbitDistance()
    {
        float elapsedTime = Time.time - Instance.startTime;
        int index = GetWaveOneMinionDistanceIndex(elapsedTime);
        return Instance.waveOneDistances[index];
    }

    public static float GetBossWaveOneOrbitDistance()
    {
        float elapsedTime = Time.time - Instance.startTime;
        int index = GetWaveOneMinionDistanceIndex(elapsedTime) + 1;
        return Instance.waveOneDistances[index];
    }

    public static void StartLevel(int newLevel)
    {
        Instance.level = newLevel;
        Instance.startTime = Time.time;

        switch (newLevel)
        {
            case 1:
                Instance.SetLevel(new Level1());
                break;
            case 2:
                Instance.SetLevel(new Level2());
                break;
            case 3:
                Instance.SetLevel(new Level3());
                break;
            default:
                Debug.LogWarning($"Level {newLevel} is not implemented.");
                break;
        }
    }

    private void SetLevel(Level level)
    {
        Instance.currentEventsMap = level.GetEventsMap();
        Instance.songTime = 0f;
    }
}