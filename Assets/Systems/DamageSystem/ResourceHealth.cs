using System;
using UnityEngine;

public class ResourceHealth : MonoBehaviour
{
    public float Current { get; private set; }
    public float Max => maxProvider?.Invoke() ?? 100f;

    private Func<float> maxProvider;

    public event Action<float, float> OnHealthChanged;
    public event Action OnHealthZero;

    public void Init(Func<float> maxProvider)
    {
        this.maxProvider = maxProvider;
        Current = Max;
        OnHealthChanged?.Invoke(Current, Max);
    }

    public void ApplyDamage(float amount)
    {
        Current = Mathf.Max(0f, Current - amount);
        OnHealthChanged?.Invoke(Current, Max);

        if (Current <= 0)
            OnHealthZero?.Invoke();
    }
}
