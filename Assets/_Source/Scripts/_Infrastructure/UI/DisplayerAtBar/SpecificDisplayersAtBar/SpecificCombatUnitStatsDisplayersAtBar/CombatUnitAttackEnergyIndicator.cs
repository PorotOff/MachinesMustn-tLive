using System.Collections.Generic;
using UnityEngine;

public class CombatUnitAttackEnergyIndicator : CombatUnitStatIndicator
{
    [SerializeField] private List<EnergyStripe> _energyStripes;

    public override void Initialize(CombatUnit combatUnit)
    {
        base.Initialize(combatUnit);
        Indicator.Initialize(CombatUnit.AttackEnergy);

        UpdateStripes();
    }

    private void UpdateStripes()
    {
        DisableAllEnergyStripes();
        EnableEnergyStripes(CombatUnit.AttackEnergy.EnergyStripesCount);
    }

    private void DisableAllEnergyStripes()
    {
        _energyStripes.ForEach(stripe => stripe.gameObject.SetActive(false));
    }

    private void EnableEnergyStripes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _energyStripes[i].gameObject.SetActive(true);
        }
    }
}