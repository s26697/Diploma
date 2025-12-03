public class StatOwner : MonoBehaviour
{
    [SerializeField] private StatDefinitionSO definition;

    private StatContainer runtimeMods;

    public float GetStat(StatType type)
    {
        float baseValue = definition.GetBaseValue(type);

        if (runtimeMods == null)
            return baseValue;

        return runtimeMods.GetFinalValue(type, baseValue);
    }

    public void AddModifier(StatType type, StatModifier modifier)
    {
        runtimeMods ??= new StatContainer();
        runtimeMods.AddModifier(type, modifier);
    }
}
