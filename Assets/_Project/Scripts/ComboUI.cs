using TMPro;
using UnityEngine;

public class ComboUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private Transform comboMeter;

    private void Awake()
    {
        if (comboText == null)
        {
            Debug.LogError("ComboText is null. Please set the reference in the inspector or check the prefab");
        }
        if (comboMeter == null)
        {
            Debug.LogError("ComboMeter is null. Please set the reference in the inspector or check the prefab");
        }
    }

    public void OnEnable()
    {
        ComboManager.OnComboChange += UpdateComboText;
    }
    private void Update()
    {
        UpdateComboMeter();
    }
    /// <summary>
    /// Updates the scale of the combo meter
    /// </summary>
    private void UpdateComboMeter()
    {
        var difference = ComboManager.Instance.Combo - ComboManager.Instance.PrevComboThreshold;
        //Hopefully difference is never negative but just in case
        if (difference < 0)
        {
            Debug.LogWarning("Combo meter change to is negative somehow. Maybe do some clamping on difference?");
            difference *= -1;
        }
        var denominator = ComboManager.Instance.NextComboThreshold - ComboManager.Instance.PrevComboThreshold;
        var proportion = denominator != 0f ? difference / denominator : 0f;
        comboMeter.localScale = new Vector3(proportion, 1, 1);
    }

    private void UpdateComboText(ComboRank comboRank)
    {
        comboText.text = comboRank.ToString();
    }
}
