using UnityEngine;

public abstract class CombatUnitView : MonoBehaviour
{
    [field: SerializeField] public CombatUnitAnimator Animator { get; private set; }
    [field: SerializeField] public CombatUnitAnimationEvents AnimationEvents { get; private set; }

    private HealthDisplayerAtBar _healthDisplayerAtBar;

    public void Initialize(HealthDisplayerAtBar healthDisplayerAtBar)
    {
        _healthDisplayerAtBar = healthDisplayerAtBar;
    }

    public void Initialize(Health health)
    {
        _healthDisplayerAtBar.Initialize(health);
    }

    public virtual void Subscribe()
    {
        _healthDisplayerAtBar.Subscribe();
    }

    public virtual void Unsubscribe()
    {
        _healthDisplayerAtBar.Subscribe();
    }
}