using System.Collections.Generic;
using UnityEngine;

public class AttackEnergyView : MonoBehaviour
{
    [SerializeField] private AnimatedBarMinToMaxValueIndicator _indicator;
    [SerializeField] private List<EnergyStripe> _energyStripes;

    private AttackEnergy _attackEnergy;

    public void Initialize(AttackEnergy attackEnergy)
    {
        _attackEnergy = attackEnergy;
        _indicator.Initialize(0, _attackEnergy.Max, _attackEnergy.Current);
        DisplayAttackEnergy();
    }

    public void Subscribe()
    {
        _attackEnergy.Changed += DisplayAttackEnergy;
    }

    public void Unsubscribe()
    {
        _attackEnergy.Changed -= DisplayAttackEnergy;
    }

    private void DisplayAttackEnergy()
    {
        _indicator.Display(_attackEnergy.Current);
        DisableAllEnergyStripes();
        EnableEnergyStripes(_attackEnergy.EnergyStripesCount);
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