using UnityEngine;

public class CombatUnitView : MonoBehaviour
{
    [field: SerializeField] public CombatUnitAnimator Animator { get; private set; }
    [field: SerializeField] public CombatUnitAnimationEvents AnimationEvents { get; private set; }
}