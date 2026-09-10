using System.Collections.Generic;
using UnityEngine;

public class AttackEnergyBar : CombatUnitStatsDisplayerAtBar
{
    [SerializeField] private List<EnergyStripe> _energyStripes;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        DisplayerAtBar.Displayed -= UpdateStripes;
    }

    public override void Initialize(CombatUnit combatUnit)
    {
        base.Initialize(combatUnit);
        DisplayerAtBar.Initialize(CombatUnit.AttackEnergy);

        UpdateStripes();
        DisplayerAtBar.Displayed += UpdateStripes;
        // todo Пофиксить все подписки на DisplayerAtBar (или переделать его нахуй по нормальному)
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