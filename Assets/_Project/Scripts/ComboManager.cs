using System;
using UnityEngine;
public enum ComboRank
{
    D,
    C,
    B,
    A,
    S,
    SS,
    SSS,
    HADOPELAGIC
}
public class ComboManager : MonoBehaviour
{
    [SerializeField] private float combo;
    [SerializeField] private float maxCombo = 1500;
    [SerializeField] private ComboRank comboRank;
    [Tooltip("Combo score loss per second")]
    [SerializeField] private float comboDrain = 10;
    [Tooltip("Combo score gained on a perfect hit")]
    [SerializeField] private float comboPerHit = 25;
    [Tooltip("Time threshold for a perfect hit both early or late")]
    [SerializeField] private float perfectTimeThreshold = .06f;
    
    public static ComboManager Instance { get; private set; }
    public float Combo => combo;
    public ComboRank ComboRank => comboRank;
    public static Action<ComboRank> OnComboChange;
    /// <summary>
    /// How many total combo points are needed to reach the next rank. Useful for a UI bar showing distance to the next rank
    /// </summary>
    public float NextComboThreshold { get; private set; }
    public float PrevComboThreshold { get; private set; }
    
    /// <summary>
    /// Array containing threshold to reach the next combo rank. The last value is the maximum combo value.
    /// Length should be equal to length of comboRanks + 1
    /// </summary>
    private static readonly float[] comboThresholds = { 0f, 2000f, 4000f, 6000f, 9000f, 12000f, 15000f, 18000f, 20000f };
    private static readonly ComboRank[] comboRanks = (ComboRank[])Enum.GetValues(typeof(ComboRank));

    private void Awake()
    {
        if (comboRanks.Length != comboThresholds.Length - 1)
        {
            Debug.LogError("ComboRank and Threshold mismatch. There should one more threshold than the number of ranks");
        }
        Instance = this;
        comboThresholds[^1] = maxCombo;
        PrevComboThreshold = 0;
        NextComboThreshold = comboThresholds[1];
    }

    private void OnEnable()
    {
        Sword.OnSwordHit += OnSwordHit;
    }

    private void OnDisable()
    {
        Sword.OnSwordHit -= OnSwordHit;
    }
    
    void Update()
    {
        //Debug stuff
        if (Input.GetKeyDown(KeyCode.Space))
        {
            combo += comboPerHit;
        }
        combo = Mathf.Clamp(combo-=comboDrain * Time.deltaTime, 0, maxCombo);
        UpdateComboRank();
    }

    private void UpdateComboRank()
    {
        // Assumes enum is ordered: C (weakest) → HADOPELAGIC (strongest)
        //We use the length of comboRanks. Assuming designers will set the right number of thresholds
        for (var i = comboRanks.Length - 1; i >= 0; i--)
        {
            print(comboThresholds[i]);
            if (combo < comboThresholds[i]) continue;
            if (comboRank == comboRanks[i]) return;
            //If the combo changes, fire off the event to let the ComboUI do a coroutine. Otherwise, since the rank doesn't change just return
            comboRank = comboRanks[i];
            NextComboThreshold = comboThresholds[i + 1];
            PrevComboThreshold = comboThresholds[i];
            OnComboChange?.Invoke(comboRank);
            return;
        }

        // Optional fallback if something goes wrong
        comboRank = ComboRank.C;
        NextComboThreshold = comboThresholds[1];
        PrevComboThreshold = 0;
    }

    private void OnSwordHit(string o)
    {
        var distance = Conductor.Instance.DistanceFromBeat();
        //For now, if the player hit within 60 ms of a beat, we call it perfect. Otherwise, we subtract the distance beyond perfect
        //Theoretically speaking, no song should have super fast BPM for this game design
        combo += comboPerHit - Mathf.Max(0, distance - perfectTimeThreshold);
    }
}
