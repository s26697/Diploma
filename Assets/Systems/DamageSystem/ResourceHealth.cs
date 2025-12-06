using UnityEngine;

public class ResourceHealth : MonoBehaviour
{
    public float Current { get; private set; }
    public float Max => maxProvider?.Invoke() ?? 100f;

    private System.Func<float> maxProvider;

    public event System.Action<float, float> OnHealthChanged; 
    public event System.Action OnHealthZero;

    public void Init(System.Func<float> maxProvider)
    {
        this.maxProvider = maxProvider;
        Current = Max;
        OnHealthChanged?.Invoke(Current, Max);
    }

    public void ApplyDamage(float amount)
    {
        Current -= amount;
        if (Current < 0f) Current = 0f;

        OnHealthChanged?.Invoke(Current, Max);

        if (Current <= 0f)
            OnHealthZero?.Invoke();
    }

    public void RestoreFull()
    {
        Current = Max;
        OnHealthChanged?.Invoke(Current, Max);
    }
}
