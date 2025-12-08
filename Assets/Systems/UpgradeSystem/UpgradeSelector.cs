using System.Linq;
using UnityEngine;

public class UpgradeSelector
{
    private readonly UpgradeDefinitionSO[] all;

    public UpgradeSelector(UpgradeDefinitionSO[] data)
    {
        all = data;
    }

    public UpgradeDefinitionSO[] GetRandomChoices(int count)
    {
        return all
            .OrderBy(_ => Random.value)
            .Take(count)
            .ToArray();
    }
}
