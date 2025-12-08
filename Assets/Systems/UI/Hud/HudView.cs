using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDView : MonoBehaviour
{
    [Header("Wave UI")]
    [SerializeField] private TMP_Text waveCounterText;
    [SerializeField] private TMP_Text waveTimerText;

    [Header("Player Health UI")]
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Image hpFill;

    public void SetWaveCounter(int waveNumber)
    {
        waveCounterText.text = $"Wave: {waveNumber + 1}";
    }

    public void SetWaveTimer(float time)
    {
        waveTimerText.text = $"{time:0.0}s";
    }

    public void SetHealth(float current, float max)
    {
        hpText.text = $"{current}/{max}";
        
    }
}
