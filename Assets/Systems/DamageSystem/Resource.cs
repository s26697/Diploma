using UnityEngine;

[System.Serializable]
public class Resource
{
    [SerializeField] private float max;
    [SerializeField] private float current;

    public float Max => max;
    public float Current => current;

    public event System.Action<float> OnChanged;

    public Resource(float max)
    {
        this.max = max;
        current = max;
    }

    public void SetMax(float value)
    {
        max = Mathf.Max(1, value);
        current = Mathf.Min(current, max);
        OnChanged?.Invoke(current);
    }

    public void SetCurrent(float value)
    {
        current = Mathf.Clamp(value, 0, max);
        OnChanged?.Invoke(current);
    }

    public void Add(float value)
    {
        SetCurrent(current + value);
    }

    public void Reduce(float value)
    {
        SetCurrent(current - value);
    }
}
