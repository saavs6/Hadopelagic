using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager Instance { get; private set; }

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

    public static void SetMinionDistance(float distance) {
        LevelManager.Instance.minionDistance = distance;
    }

    public static void SetBossDistance(float distance) {
        LevelManager.Instance.bossDistance = distance;
    }

    public static void SetIsAttacking(bool isAttacking)
    {
        LevelManager.Instance.bossAttacking = isAttacking;
    }

    public static void SetIsTailWhipping(bool isTailWhipping)
    {
        LevelManager.Instance.bossTailWhipping = isTailWhipping;
    }

    public static void SetIsSwarming(bool isSwarming)
    {
        LevelManager.Instance.swarm = isSwarming;
    }
}