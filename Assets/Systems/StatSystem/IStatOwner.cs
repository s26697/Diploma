public interface IStatOwner
{
    float GetStat(StatType type);
    void AddModifier(StatType type, StatModifier mod);
}
