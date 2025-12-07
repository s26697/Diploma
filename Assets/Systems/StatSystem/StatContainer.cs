using System.Collections.Generic;

public class StatContainer
{
    private readonly Dictionary<StatType, List<StatModifier>> mods = new();

    public void Add(StatType type, StatModifier mod)
    {
        if (!mods.TryGetValue(type, out var list))
        {
            list = new List<StatModifier>();
            mods[type] = list;
        }

        list.Add(mod);
    }

    public void Remove(StatType type, StatModifier mod)
    {
        if (mods.TryGetValue(type, out var list))
        {
            list.Remove(mod);
        }
    }

    public void ClearType(StatType type)
    {
        if (mods.ContainsKey(type))
            mods[type].Clear();
    }

    public void ClearAll() => mods.Clear();

    public IReadOnlyList<StatModifier> GetModifiers(StatType type)
    {
        if (mods.TryGetValue(type, out var list))
            return list;
        return null;
    }
}
