[CreateAssetMenu]
public class StatDefinitionSO : ScriptableObject 
{
    [Serializable]
    public struct Entry
    {
        public StatType type;
        public float baseValue;
    }

    public Entry[] entries;

    private Dictionary<StatType, float> _dict;

    /
    public float GetBaseValue(StatType type)
    {
        if (_dict == null)
        {
            _dict = new Dictionary<StatType, float>();      
            foreach (var e in entries) _dict[e.type] = e.baseValue; 
        }

        return _dict[type];
    }
}