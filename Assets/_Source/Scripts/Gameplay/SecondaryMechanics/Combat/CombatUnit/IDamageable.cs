public interface IDamageable
{
    public bool IsDead { get; }
    
    public void TakeDamage(int damage);
}