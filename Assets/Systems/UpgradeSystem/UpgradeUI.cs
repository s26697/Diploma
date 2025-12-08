using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject root;
    [SerializeField] private UpgradeButton[] buttons;

    private void Awake()
{
    Hide();
}


    public void Show(UpgradeDefinitionSO[] upgrades, System.Action<UpgradeDefinitionSO> onSelect)
    {
        root.SetActive(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].Set(upgrades[i], onSelect);
        }
    }

    public void Hide()
    {
        root.SetActive(false);
    }
}
