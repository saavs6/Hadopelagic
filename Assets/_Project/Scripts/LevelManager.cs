using UnityEngine;

public class LevelManager : MonoBehaviour
{
    
    public static LevelManager Instance { get; private set; }

    public float[] waveOneDistances;

    float startTime;
    int level = -1;

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
        musicPlayer = GetComponent<AudioSource>();
        StartLevel(1);
    }

    void Update()
    {

    }

    public static float GetElapsedTime() {
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

    public static void StartLevel(int newLevel) {
        Instance.level = newLevel;
        Instance.startTime = Time.time;
    }

}