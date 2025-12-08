using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text desc;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;

    private UpgradeDefinitionSO data;

    public void Set(UpgradeDefinitionSO d, System.Action<UpgradeDefinitionSO> onClick)
    {
        data = d;

        title.text = d.UpgradeName;
        desc.text = d.Description;
        icon.sprite = d.Icon;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClick(data));
    }
}
