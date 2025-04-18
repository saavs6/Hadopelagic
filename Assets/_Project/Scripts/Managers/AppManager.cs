using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LevelManager.StartLevel(1);
    }
}
