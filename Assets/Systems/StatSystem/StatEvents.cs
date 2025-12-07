using System;

public static class StatEvents
{
    public static event Action<StatOwner, StatType, float> OnStatChanged;

    public static void RaiseStatChanged(StatOwner owner, StatType type, float value)
        => OnStatChanged?.Invoke(owner, type, value);
}
