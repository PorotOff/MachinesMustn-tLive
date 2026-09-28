using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : MonoBehaviour, IPooledObject<CombatUnit>, IDamageable, IPurchasable
{
    private Attacker _attacker;
    private DamageTaker _damageTaker;

    public event Action<CombatUnit> Released;
    public event Action Died;

    public CombatUnitConfig Config { get; private set; }
    public CombatUnitView View { get; private set;}
    public HealthStat Health { get; private set; }
    public AttackEnergyStat AttackEnergy { get; private set; }
    public bool IsDead => Health.Current == 0;
    public ICombatUnitState State { get; private set; }

    public void Initialize(CombatUnitConfig config, CombatUnitView view)
    {
        Config = config;
        View = view;

        _attacker = Config.Attacker;
        _attacker.Initialize(this);

        _damageTaker = Config.DamageTaker;
        _damageTaker.Initialize(this);

        Health = new HealthStat(Config.MinHealth, Config.MaxHealth, Config.Health);
        AttackEnergy = new AttackEnergyStat(Config.EnergyStripeCapacity, Config.EnergyStripesCount, Config.MinAttackEnergy, Config.MaxAttackEnergy, Config.AttackEnergy);

        SetState(new IdleCombatUnitState(this));
        Subscribe();
    }

    public void Release()
    {
        Released?.Invoke(this);
    }

    public void Attack(List<CombatUnit> opponents)
    {
        SetState(new AttackCombatUnitState(this, _attacker, opponents));
    }

    public void TakeDamage(int damage)
    {
        SetState(new TakingDamageCombatUnitState(this, _damageTaker, damage));
    }

    public void StandAtPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetState(ICombatUnitState state)
    {
        State?.Exit();
        State = state;
        State.Enter();
    }

    private void Subscribe()
    {
        View.AnimationEvents.TakeDamageAnimationComplete += OnTakeDamageAnimationComplete;
        View.AnimationEvents.DieAnimationComplete += OnDieAnimationComplete;
    }

    private void Unsubscribe()
    {
        View.AnimationEvents.TakeDamageAnimationComplete -= OnTakeDamageAnimationComplete;
        View.AnimationEvents.DieAnimationComplete -= OnDieAnimationComplete;
    }

    private void OnTakeDamageAnimationComplete()
    {
        if (Health.Current == 0)
        {
            SetState(new DeadCombatUnitState(this));
            return;
        }

        SetState(new IdleCombatUnitState(this));
    }

    private void OnDieAnimationComplete()
    {
        Unsubscribe();
    }
}