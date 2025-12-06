public struct DamageInfo
{
    public float Amount;
    public IDamaging Source;

    public DamageInfo(float amount, IDamaging source)
    {
        Amount = amount;
        Source = source;
    }
}
