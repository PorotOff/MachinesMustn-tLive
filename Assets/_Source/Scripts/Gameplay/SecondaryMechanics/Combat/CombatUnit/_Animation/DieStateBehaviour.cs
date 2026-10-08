using UnityEngine;

public class DieStateBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.TryGetComponent(out CombatUnitAnimationEvents combatUnitAnimationEvents))
        {
            combatUnitAnimationEvents.InvokeDieAnimationComplete();
        }
    }
}