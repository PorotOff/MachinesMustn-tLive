public interface IDamageable
{
    public bool IsDied { get; }
    
    public void TakeDamage(int damage);
}