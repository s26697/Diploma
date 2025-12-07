public static class StatCalculator
{
    public static float CalculateFinal(StatContainer mods, StatDefinitionSO defs, StatType type)
    {
        float baseValue = defs.GetBaseValue(type);

        if (mods == null)
            return baseValue;

        float add = 0f;
        float mult = 1f;

        var list = mods.GetModifiers(type);
        if (list == null)
            return baseValue;

        foreach (var m in list)
        {
            if (m.Type == StatModType.Additive)
                add += m.Value;
            else
                mult *= (1 + m.Value);
        }

        return (baseValue + add) * mult;
    }
}
