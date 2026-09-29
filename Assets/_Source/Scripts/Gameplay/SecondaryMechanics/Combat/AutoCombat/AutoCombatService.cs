using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoCombatService
{
    private MonoBehaviour _monoBehaviour;

    private AutoCombat _autoCombat;
    private Dictionary<Queue<CombatUnit>, List<CombatUnit>> _attackersOpponents = new Dictionary<Queue<CombatUnit>, List<CombatUnit>>();

    public AutoCombatService(MonoBehaviour monoBehaviour)
    {
        if (monoBehaviour == null)
            throw new ArgumentNullException(nameof(monoBehaviour));

        _monoBehaviour = monoBehaviour;
    }

    public event Action CombatOver;

    public void AddCombat(Queue<CombatUnit> attackers, List<CombatUnit> opponents)
    {
        _attackersOpponents.Add(attackers, opponents);
    }

    public void StartCombat()
    {
        Unsubscribe();

        if (_attackersOpponents.Count == 0)
        {
            InvokeCombatOver();
            return;
        }

        var attackersOpponentsPair = _attackersOpponents.First();
        Queue<CombatUnit> attackers = attackersOpponentsPair.Key;
        List<CombatUnit> opponents = attackersOpponentsPair.Value;

        _attackersOpponents.Remove(attackers);

        _autoCombat = new AutoCombat(attackers, opponents, _monoBehaviour);

        Subscribe();
    }

    private void Subscribe()
    {
        _autoCombat.CombatOver += StartCombat;
        _autoCombat.OpponentsDied += InvokeCombatOver;
    }

    private void Unsubscribe()
    {
        if (_autoCombat == null)
            return;

        _autoCombat.CombatOver -= StartCombat;
        _autoCombat.OpponentsDied -= InvokeCombatOver;
    }

    private void InvokeCombatOver()
    {
        CombatOver?.Invoke();
    }
}