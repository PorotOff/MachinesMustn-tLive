using System.Collections.Generic;
using System.Linq;

public class BattlePhase : IPhase
{
    private PhaseService _phaseService;
    private CombatUnitRepository _combatUnitRepository;
    private AutoCombatService _autoCombatService;

    public BattlePhase(PhaseService phaseService, CombatUnitRepository combatUnitRepository, AutoCombatService autoCombatService)
    {
        _phaseService = phaseService;
        _combatUnitRepository = combatUnitRepository;
        _autoCombatService = autoCombatService;
    }

    public void Enter()
    {
        Subscribe();

        AddCombat(_combatUnitRepository.Warriors, _combatUnitRepository.Enemies);
        AddCombat(_combatUnitRepository.Enemies, _combatUnitRepository.Warriors);
        _autoCombatService.StartCombat();
    }

    public void Exit()
    {
        Unsubscribe();
    }

    private void AddCombat(IReadOnlyList<CombatUnit> attackers, IReadOnlyList<CombatUnit> opponents)
    {
        List<CombatUnit> aliveAttackers = attackers.Where(attacker => attacker.IsDead == false).ToList();
        Queue<CombatUnit> aliveAttackersQueue = new Queue<CombatUnit>(aliveAttackers);

        List<CombatUnit> aliveOpponents = opponents.Where(opponent => opponent.IsDead == false).ToList();
        
        _autoCombatService.AddCombat(aliveAttackersQueue, aliveOpponents);
    }

    private void Subscribe()
    {
        _autoCombatService.CombatOver += OnCombatOver;
        _autoCombatService.OpponentsDied += OnAnyCombatUnitsDied;
    }

    private void Unsubscribe()
    {
        _autoCombatService.CombatOver -= OnCombatOver;
        _autoCombatService.OpponentsDied -= OnAnyCombatUnitsDied;
    }

    private void OnCombatOver()
    {
        _phaseService.SetPreparePhase();
    }

    private void OnAnyCombatUnitsDied()
    {
        Exit();

        if (IsAllCombatUnitsDead(_combatUnitRepository.Warriors))
        {
            _phaseService.InvokeWarriorsDied();
            return;
        }

        if (IsAllCombatUnitsDead(_combatUnitRepository.Enemies))
        {
            _phaseService.InvokeEnemiesDied();
            return;
        }
    }

    private bool IsAllCombatUnitsDead(IReadOnlyList<CombatUnit> combatUnits)
    {
        return combatUnits.All(combatUnit => combatUnit.State is DeadCombatUnitState);
    }
}