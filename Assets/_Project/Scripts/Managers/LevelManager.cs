using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }


    public float minionDistance = 0f;
    public bool swarm = false;
    
    public float bossDistance = 0f;
    public bool bossAttacking = false;
    public bool bossTailWhipping = false;
    public bool bossCharging = false;

    public int poly = 3;
    
    public int shieldHitPoints = 8;
    public int maxShieldHitPoints = 8;
    public float startTime;
    public float songTime;
    public int level = 0;

    private Image shieldImage;
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Instance.musicPlayer = GetComponent<AudioSource>();
        Instance.shieldImage = GameObject.Find("Shield").GetComponent<Image>();
        Instance.shieldHitPoints = Instance.maxShieldHitPoints;
        UpdateShieldUI();
    }

    void Update()
    {
        if (currentEventsList == null) return;
        if (currentEventsList.Count == 0) return;
        
        SongEvent eventData = currentEventsList[0];
        if (eventData != null && Conductor.Instance.songPosition >= eventData.time)
        {
            currentEventsList.RemoveAt(0);
            foreach (var action in eventData.actions)
            {
                action?.Invoke();
            }
        }
        Instance.songTime += Time.deltaTime;
    }

    public static void removeShield(int hitpoints) {
        Instance.shieldHitPoints -= hitpoints;
        if (Instance.shieldHitPoints < 0) {
            Instance.shieldHitPoints = 0;
        }
        Instance.UpdateShieldUI();
    }

    public static void addShield(int hitpoints) {
        Instance.shieldHitPoints += hitpoints;
        if (Instance.shieldHitPoints > 8) {
            Instance.shieldHitPoints = 8;
        }
        Instance.UpdateShieldUI();
    }

    private void UpdateShieldUI() {
        if (Instance.shieldImage != null) {
            float fillAmount = (float) shieldHitPoints / maxShieldHitPoints;
            Instance.shieldImage.fillAmount = Mathf.Clamp01(fillAmount);
        }
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