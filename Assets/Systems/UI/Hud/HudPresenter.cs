using UnityEngine;

public class HUDPresenter : MonoBehaviour
{
    [SerializeField] private HUDView view;   

    private void OnEnable()
    {
        GameEvents.OnWaveStarted += HandleWaveStarted;
        GameEvents.OnWaveTimerTick += HandleWaveTimerTick;
        GameEvents.OnPlayerHealthChanged += HandleHealthChanged;
    }

    private void OnDisable()
    {
        GameEvents.OnWaveStarted -= HandleWaveStarted;
        GameEvents.OnWaveTimerTick -= HandleWaveTimerTick;
        GameEvents.OnPlayerHealthChanged -= HandleHealthChanged;
    }

    private void HandleWaveStarted(int wave)
    {
        view.SetWaveCounter(wave);
    }

    private void HandleWaveTimerTick(int seconds)
    {
        view.SetWaveTimer(seconds);
    }

    private void HandleHealthChanged(float current, float max)
    {
        view.SetHealth(current, max);
    }
}
