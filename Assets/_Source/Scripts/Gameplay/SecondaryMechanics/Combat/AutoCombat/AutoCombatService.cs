using System;
using System.Collections.Generic;
using System.Linq;

public class AutoCombatService
{
    private CoroutineService _coroutineService;

    private AutoCombat _autoCombat;
    private Dictionary<Queue<CombatUnit>, List<CombatUnit>> _attackersOpponents = new Dictionary<Queue<CombatUnit>, List<CombatUnit>>();

    public AutoCombatService(CoroutineService coroutineService)
    {
        _coroutineService = coroutineService;
    }

    public event Action CombatOver;
    public event Action OpponentsDied;

    public void AddCombat(Queue<CombatUnit> attackers, List<CombatUnit> opponents)
    {
        _attackersOpponents.Add(attackers, opponents);
    }

    public void StartCombat()
    {
        Unsubscribe();

        var attackersOpponentsPair = _attackersOpponents.First();
        Queue<CombatUnit> attackers = attackersOpponentsPair.Key;
        List<CombatUnit> opponentns = attackersOpponentsPair.Value;

        _attackersOpponents.Remove(attackers);

        Queue<CombatUnit> aliveAttackers = new Queue<CombatUnit>(attackers.Where(attacker => attacker.State is not DeadCombatUnitState));
        List<CombatUnit> aliveOpponents = opponentns.Where(opponent => opponent.State is not DeadCombatUnitState).ToList();

        _autoCombat = new AutoCombat(aliveAttackers, aliveOpponents, _coroutineService);
        _autoCombat.StartCombat();

        Subscribe();
    }

    private void Subscribe()
    {
        _autoCombat.CombatOver += OnCombatOver;
        _autoCombat.OpponentsDied += InvokeOpponentsDied;
    }

    private void Unsubscribe()
    {
        if (_autoCombat == null)
            return;

        _autoCombat.CombatOver -= OnCombatOver;
        _autoCombat.OpponentsDied -= InvokeOpponentsDied;
    }

    private void OnCombatOver()
    {
        if (_attackersOpponents.Count == 0)
        {
            Unsubscribe();
            CombatOver?.Invoke();
            
            return;
        }

        StartCombat();
    }

    private void InvokeOpponentsDied()
    {
        OpponentsDied?.Invoke();
    }
}