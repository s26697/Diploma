using UnityEngine;

[CreateAssetMenu(menuName = "Game/Upgrade Definition")]
public class UpgradeDefinitionSO : ScriptableObject
{
    public string UpgradeName;
    public string Description;
    public Sprite Icon;

    public StatType TargetStat;
    public StatModifier Modifier;
}
