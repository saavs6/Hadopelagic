using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public float minionDistance = 0f;
    public float bossDistance = 0f;

    public float startTime;
    public float songTime;
    public int level = -1;

    private List<SongEvent> currentEventsList;
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
        if (currentEventsList == null) return;
        if (currentEventsList.Count == 0) return;
        
        SongEvent eventData = currentEventsList[0];
        if (eventData != null && Instance.songTime >= eventData.time)
        {
            Debug.Log("Next actions");
            currentEventsList.RemoveAt(0);
            foreach (var action in eventData.actions)
            {
                action?.Invoke();
            }
        }
        Instance.songTime += Time.deltaTime;
    }

    public static float GetElapsedTime()
    {
        return Time.time - Instance.startTime;
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
        Instance.songTime = 0f;
        Instance.currentEventsList = level.GetEventsList();
    }
}