using UnityEngine;

public abstract class CombatUnit : MonoBehaviour, IDamageable
{
    [SerializeField] private CombatUnitConfig _config;

    private Health _health;
    protected AttackEnergy AttackEnergy;

    protected virtual void Awake()
    {
        AttackEnergy = new AttackEnergy(_config.EnergyStripeCapacity, _config.EnergyStripesCount);
    }

    protected virtual void OnEnable()
    {
        
    }

    protected virtual void OnDisable()
    {
        
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    public void Attack(IDamageable damageable)
    {
        damageable.TakeDamage(_config.Damage);
    }
}