using UnityEngine;

public abstract class CombatUnit : MonoBehaviour, IDamageable
{
    [SerializeField] private CombatUnitConfig _config;
    [SerializeField] private HealthDisplayerAtBar _healthDisplayerAtBar;

    private Health _health;
    protected AttackEnergy AttackEnergy;

    protected virtual void Awake()
    {
        _health = new Health();
        AttackEnergy = new AttackEnergy(_config.EnergyStripeCapacity, _config.EnergyStripesCount);

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

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    public void Attack(IDamageable damageable)
    {
        damageable.TakeDamage(_config.Damage);
    }
}