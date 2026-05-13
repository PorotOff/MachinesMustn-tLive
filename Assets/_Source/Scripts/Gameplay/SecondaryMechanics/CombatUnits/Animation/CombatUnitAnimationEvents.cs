using System;
using UnityEngine;

public class CombatUnitAnimationEvents : MonoBehaviour
{
    public event Action Attacked;
    public event Action TakedDamage;

    public void InvokeAttacked()
    {
        Attacked?.Invoke();
    }

    public void InvokeTakedDamage()
    {
        TakedDamage?.Invoke();
    }
}