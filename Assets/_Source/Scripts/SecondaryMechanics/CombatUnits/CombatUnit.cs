using UnityEngine;

public abstract class CombatUnit : MonoBehaviour, IDamageable
{
    private CombatUnitConfig _config;

    private Health _health;

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    public void Attack(IDamageable damageable)
    {
        damageable.TakeDamage(_config.Damage);
    }
}