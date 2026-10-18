using System;
using UnityEngine;

public class PhaseService
{
    private PreparePhaseFactory _preparePhaseFactory;
    private BattlePhaseFactory _battlePhaseFactory;

    public PhaseService(PreparePhaseFactory preparePhaseFactory, BattlePhaseFactory battlePhaseFactory)
    {
        _preparePhaseFactory = preparePhaseFactory;
        _battlePhaseFactory = battlePhaseFactory;
    }

    public event Action WarriorsDied;
    public event Action EnemiesDied;

    public IPhase Phase;

    public void SetPreparePhase()
    {
        SetPhase(_preparePhaseFactory.Create());
    }

    public void SetBattlePhase()
    {
        SetPhase(_battlePhaseFactory.Create());
    }

    public void InvokeWarriorsDied()
    {
        WarriorsDied?.Invoke();
    }

    public void InvokeEnemiesDied()
    {
        EnemiesDied?.Invoke();
    }

    private void SetPhase(IPhase phase)
    {
        Phase?.Exit();

        Phase = phase;
        Debug.Log($"{nameof(Phase)} = {Phase}");

        Phase.Enter();
    }
}