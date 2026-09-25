using System;
using UnityEngine;

public class CombatUnitAnimationEvents : MonoBehaviour
{
    public event Action Attacked;
    public event Action TakingDamageFinished;

    public void InvokeAttacked()
    {
        Attacked?.Invoke();
    }

    public void InvokeTakingDamageFinished()
    {
        TakingDamageFinished?.Invoke();
    }
}