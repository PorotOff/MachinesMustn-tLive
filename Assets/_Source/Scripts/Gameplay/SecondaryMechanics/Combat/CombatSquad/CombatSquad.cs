using System;
using System.Collections.Generic;
using System.Linq;

public class CombatSquad
{
    private List<CombatUnit> _combatUnits;

    public event Action Died;

    public List<CombatUnit> CombatUnits => new List<CombatUnit>(_combatUnits);

    public CombatSquad(List<CombatUnit> combatUnits)
    {
        combatUnits.CastExeption();
        _combatUnits = combatUnits;

        OrderByDescending();
        Subscribe();
    }

    private void Subscribe()
    {
        foreach (var unit in _combatUnits)
        {
            unit.Attacked += OnCombatUnitAttacked;
            unit.Died += OnCombatUnitDied;
        }
    }

    private void Unsubscribe()
    {
        foreach (var unit in _combatUnits)
        {
            unit.Attacked -= OnCombatUnitAttacked;
            unit.Died -= OnCombatUnitDied;
        }
    }

    private void UnsubscribeDeadUnits()
    {
        List<CombatUnit> deadUnits = _combatUnits.Where(unit => unit.IsDead).ToList();

        foreach (var deatUnit in deadUnits)
        {
            deatUnit.Attacked -= OnCombatUnitAttacked;
            deatUnit.Died -= OnCombatUnitDied;
        }
    }

    private void OnCombatUnitAttacked()
    {
        throw new NotImplementedException();
    }

    private void OnCombatUnitDied()
    {
        UnsubscribeDeadUnits();
        RemoveDead();

        if (_combatUnits.Count == 0)
        {
            Unsubscribe();
            Died?.Invoke();
            return;
        }

        OrderByDescending();
    }

    private void OrderByDescending()
    {
        _combatUnits.OrderByDescending(unit => unit.Config.AttackSpeed);
    }

    private void RemoveDead()
    {
        _combatUnits.RemoveAll(unit => unit.IsDead);
    }
}