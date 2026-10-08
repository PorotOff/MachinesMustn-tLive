using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CombatUnitAnimator : MonoBehaviour
{
    private readonly int Idle = Animator.StringToHash(nameof(Idle));
    private readonly int Attack = Animator.StringToHash(nameof(Attack));
    private readonly int TakeDamage = Animator.StringToHash(nameof(TakeDamage));
    private readonly int Die = Animator.StringToHash(nameof(Die));

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayIdle()
    {
        _animator.SetTrigger(Idle);
    }

    public void PlayAttack()
    {
        _animator.SetTrigger(Attack);
    }

    public void PlayTakeDamage()
    {
        _animator.SetTrigger(TakeDamage);
    }

    public void PlayDie()
    {
        _animator.SetTrigger(Die);
    }
}