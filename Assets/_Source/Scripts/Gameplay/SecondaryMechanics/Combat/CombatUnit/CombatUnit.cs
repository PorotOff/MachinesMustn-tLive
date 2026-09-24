using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : MonoBehaviour, IPooledObject<CombatUnit>, IDamageable, IPurchasable
{
    private Attacker _attacker;

    private CombatUnitView _view;

    public event Action<CombatUnit> Released;
    public event Action Attacked;
    public event Action TakedDamage;
    public event Action Died;

    public CombatUnitConfig Config { get; private set; }
    public HealthStat Health { get; private set; }
    public AttackEnergyStat AttackEnergy { get; private set; }
    public bool IsDead => Health.Current == 0;
    public bool IsBattling { get; private set; }

    public void Initialize(CombatUnitConfig config, CombatUnitView view)
    {
        Config = config;

        _attacker = Config.Attacker;
        _attacker.Initialize(this);
        
        _view = view;

        Health = new HealthStat(Config.MinHealth, Config.MaxHealth, Config.Health);
        AttackEnergy = new AttackEnergyStat(Config.EnergyStripeCapacity, Config.EnergyStripesCount, Config.MinAttackEnergy, Config.MaxAttackEnergy, Config.AttackEnergy);
    }

    public void Release()
    {
        Released?.Invoke(this);
    }

    public void Attack(List<CombatUnit> opponents)
    {
        while (AttackEnergy.AvailableAttacks > 0)
        {
            IsBattling = true;
            _attacker.Attack(opponents);
            IsBattling = false;
            
            Attacked?.Invoke();
        }
    }

    public void TakeDamage(int damage)
    {
        IsBattling = true;
        Health.Reduce(damage);
        IsBattling = false;

        TakedDamage?.Invoke();

        if (Health.Current == 0)
        {
            Died?.Invoke();
        }
    }

    public void StandAtPosition(Vector3 position)
    {
        transform.position = position;
    }
}