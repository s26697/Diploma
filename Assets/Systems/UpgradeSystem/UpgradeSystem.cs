using UnityEngine;


public class UpgradeSystem : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private UpgradeDefinitionSO[] allUpgrades;


    [Header("UI")]
    [SerializeField] private UpgradeUI upgradeUI;

    [Header("Refs")]
    [SerializeField] private StatOwner playerStats;
    [Header("xp")]
    [SerializeField] private float xpToLevelUp;
    private XPTracker xpTracker;
    private UpgradeSelector selector;

    private void Awake()
    {
        xpTracker = new XPTracker(xpToLevelUp);         
        selector = new UpgradeSelector(allUpgrades);
    }

    private void OnEnable()
    {
        GameEvents.OnPlayerGainedXP += HandleXP;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerGainedXP -= HandleXP;
    }

    private void HandleXP(float amount)
    {
        if (xpTracker.AddXP(amount))
        {
            
            var choices = selector.GetRandomChoices(3);
            ShowUpgradeSelection(choices);
        }
    }

    private void ShowUpgradeSelection(UpgradeDefinitionSO[] choices)
    {
        Time.timeScale = 0f;

        upgradeUI.Show(choices, ApplyUpgrade);
    }

    private void ApplyUpgrade(UpgradeDefinitionSO upgrade)
    {
        
        playerStats.AddModifier(upgrade.TargetStat, upgrade.Modifier);

        upgradeUI.Hide();
        Time.timeScale = 1f;
    }
}
