using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private UpgradeDefinitionSO[] allUpgrades;

    [Header("UI")]
    [SerializeField] private UpgradeUI upgradeUI;

    [Header("Refs")]
    [SerializeField] private StatOwner playerStats;

    [Header("XP")]
    [SerializeField] private float xpToLevelUp = 100f;

    [Header("AUDIO")] [SerializeField] private AudioClip lvlupSFX;
    private XPTracker xpTracker;
    private UpgradeSelector selector;

    private void Awake()
    {
        xpTracker = new XPTracker(xpToLevelUp);
        xpTracker.OnLevelUp += HandleLevelUp;

        selector = new UpgradeSelector(allUpgrades);
    }

    private void OnDestroy()
    {
        xpTracker.OnLevelUp -= HandleLevelUp;
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
        xpTracker.AddXP(amount);
    }

    private void HandleLevelUp(int newLevel)
    {
        if(lvlupSFX) AudioEvents.PlaySfx(lvlupSFX);
        var choices = selector.GetRandomChoices(3);
        ShowUpgradeSelection(choices);
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