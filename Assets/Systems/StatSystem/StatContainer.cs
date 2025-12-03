public class StatContainer
{
    private readonly Dictionary<StatType, List<StatModifier>> _mods = new();

    public void AddModifier(StatType type, StatModifier mod)
    {
        if (!_mods.ContainsKey(type))
            _mods[type] = new List<StatModifier>();

        _mods[type].Add(mod);
    }

    public float GetFinalValue(StatType stat, float baseValue)
    {
        if (!_mods.TryGetValue(stat, out var list))
            return baseValue;

        float add = 0f;
        float mult = 1f;

        foreach (var m in list)
        {
            if (m.Type == StatModType.Additive)
                add += m.Value;
            else
                mult *= 1 + m.Value;
        }

        return (baseValue + add) * mult;
    }
}
