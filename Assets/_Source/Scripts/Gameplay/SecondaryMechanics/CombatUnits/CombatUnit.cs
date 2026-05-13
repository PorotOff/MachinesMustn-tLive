using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatUnit : MonoBehaviour, IPooledObject<CombatUnit>, IDamageable, IPurchasable
{
    protected Health Health;
    protected AttackEnergy AttackEnergy;

    public event Action<CombatUnit> Released;
    public event Action Attacked;
    public event Action TakedDamage;

    public CombatUnitConfig Config { get; private set; }
    public bool IsDied => Health.Current == 0;
    public bool IsBattling { get; private set; }

    public void Initialize(CombatUnitConfig Config)
    {
        Health = new Health();
        AttackEnergy = new AttackEnergy(Config.EnergyStripeCapacity, Config.EnergyStripesCount, 100); // Temp 100
    }

    public void Release()
    {
        Unsubscribe();
        Released?.Invoke(this);
    }

    public void TakeDamage(int damage)
    {
        IsBattling = true;

        Health.TakeDamage(damage);

        IsBattling = false;
        InvokeTakedDamage();
    }

    public virtual void Attack(List<CombatUnit> opponents)
    {
        IsBattling = true;

        int randomOpponentIndex = UnityEngine.Random.Range(0, opponents.Count);
        CombatUnit opponent = opponents[randomOpponentIndex];

        opponent.TakeDamage(Config.Damage);
        SpendEnergy(Config.EnergyStripeCapacity);

        IsBattling = false;
        InvokeAttacked();
    }

    protected void SpendEnergy(int amount)
    {
        AttackEnergy.Remove(amount);
    }

    protected void InvokeAttacked()
    {
        Attacked?.Invoke();
    }

    protected void InvokeTakedDamage()
    {
        TakedDamage?.Invoke();
    }

    protected abstract void Subscribe();
    protected abstract void Unsubscribe();
}