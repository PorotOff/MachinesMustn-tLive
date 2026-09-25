using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnit : MonoBehaviour, IPooledObject<CombatUnit>, IDamageable, IPurchasable
{
    private Attacker _attacker;

    public event Action<CombatUnit> Released;
    public event Action Died;

    public CombatUnitConfig Config { get; private set; }
    public CombatUnitView View { get; private set;}
    public HealthStat Health { get; private set; }
    public AttackEnergyStat AttackEnergy { get; private set; }
    public bool IsDead => Health.Current == 0;

    public void Initialize(CombatUnitConfig config, CombatUnitView view)
    {
        Config = config;

        _attacker = Config.Attacker;
        _attacker.Initialize(this);
        
        View = view;

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
            _attacker.Attack(opponents);
        }

        // todo Заменить цикл на обработчики события. И вот там проверять, если осталась ещё энергия, то атаковать опять.
        // todo Реализовать состояния юнитов, чтобы удобоно проверять, находится ли юнит в бою или уже отстрелялся.
    }

    public void TakeDamage(int damage)
    {
        Health.Reduce(damage);

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