using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatUnit : MonoBehaviour, IPooledObject<CombatUnit>, IDamageable, IPurchasable
{
    [SerializeField] private HealthDisplayerAtBar _healthDisplayerAtBar;

    [field: SerializeField] public CombatUnitConfig Config { get; private set; }

    private Health _health;
    
    protected AttackEnergy AttackEnergy;

    public event Action<CombatUnit> Released;
    public event Action AttackComplete;
    public event Action TakingDamageComplete;

    // public IReadOnlyHealth Health => _health;
    public bool IsDied => _health.Current == 0;
    public bool IsBattling { get; private set; }

    protected virtual void Awake()
    {
        _health = new Health();
        AttackEnergy = new AttackEnergy(Config.EnergyStripeCapacity, Config.EnergyStripesCount, 100); // Temp 100

        _healthDisplayerAtBar.Initialize(_health);
    }

    protected virtual void OnEnable()
    {
        _healthDisplayerAtBar.Subscribe();
    }

    protected virtual void OnDisable()
    {
        _healthDisplayerAtBar.Unsubscribe();
    }

    public void Release()
    {
        Released?.Invoke(this);
    }

    public void TakeDamage(int damage)
    {
        IsBattling = true;

        _health.TakeDamage(damage);

        IsBattling = false;
        InvokeTakingDamageComplete();
    }

    public virtual void Attack(List<CombatUnit> opponents)
    {
        IsBattling = true;

        int randomEnemyIndex = UnityEngine.Random.Range(0, opponents.Count);
        CombatUnit opponent = opponents[randomEnemyIndex];

        opponent.TakeDamage(Config.Damage);
        SpendEnergy(Config.EnergyForAttack);

        IsBattling = false;
        InvokeAttackComplete();
    }

    protected void SpendEnergy(int amount)
    {
        AttackEnergy.Remove(amount);
    }

    protected void InvokeAttackComplete()
    {
        AttackComplete?.Invoke();
    }

    protected void InvokeTakingDamageComplete()
    {
        TakingDamageComplete?.Invoke();
    }
}