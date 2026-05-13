using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CombatUnitAnimator : MonoBehaviour
{
    private readonly int Idle = Animator.StringToHash(nameof(Idle));
    private readonly int Attack = Animator.StringToHash(nameof(Attack));
    private readonly int TakeDamage = Animator.StringToHash(nameof(TakeDamage));

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetIdle()
    {
        _animator.SetTrigger(Idle);
    }

    public void SetAttack()
    {
        _animator.SetTrigger(Attack);
    }

    public void SetTakeDamage()
    {
        _animator.SetTrigger(TakeDamage);
    }
}