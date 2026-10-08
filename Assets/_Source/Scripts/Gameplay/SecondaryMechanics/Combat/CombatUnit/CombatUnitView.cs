using UnityEngine;

[RequireComponent(typeof(CombatUnitAnimator))]
[RequireComponent(typeof(CombatUnitAnimationEvents))]
public class CombatUnitView : MonoBehaviour
{
    public CombatUnitAnimator Animator { get; private set; }
    public CombatUnitAnimationEvents AnimationEvents { get; private set; }

    public void Initialize()
    {
        Animator = GetComponent<CombatUnitAnimator>();
        AnimationEvents = GetComponent<CombatUnitAnimationEvents>();
    }
}