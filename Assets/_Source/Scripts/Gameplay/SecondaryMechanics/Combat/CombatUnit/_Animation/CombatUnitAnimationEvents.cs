using System;
using UnityEngine;

public class CombatUnitAnimationEvents : MonoBehaviour
{
    public event Action Attacked;
    public event Action TakeDamageAnimationComplete;
    public event Action DieAnimationComplete;

    public void InvokeAttacked()
    {
        Attacked?.Invoke();
    }

    public void InvokeTakeDamageAnimationComplete()
    {
        TakeDamageAnimationComplete?.Invoke();
    }

    public void InvokeDieAnimationComplete()
    {
        DieAnimationComplete?.Invoke();
    }
}