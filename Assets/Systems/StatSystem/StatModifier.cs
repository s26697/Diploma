public class StatModifier
{
    public readonly float Value;
    public readonly StatModType Type; // additive / multiplayer

    public StatModifier(float value, StatModType type)
    {
        Value = value;
        Type = type;
    }
}
