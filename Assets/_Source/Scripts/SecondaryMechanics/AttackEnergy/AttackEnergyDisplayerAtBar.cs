using System.Collections.Generic;
using UnityEngine;

public class AttackEnergyDisplayerAtBar : DisplayerAtBar<AttackEnergy>
{
    [SerializeField] private List<EnergyStripe> _energyStripes;

    protected override void Display()
    {
        base.Display();
        DisableAllEnergyStripes();
        EnableEnergyStripes(Displayeable.EnergyStripesCount);
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