using System;
using UnityEngine;

public class Health
{
    private int _value;

    public Health()
    {
        Reset();
    }

    public event Action Changed;
    public event Action BecameZero;

    public int Max => 100;
    public int Value
    {
        get => _value;

        private set
        {
            _value = Mathf.Clamp(value, 0, Max);
            Changed?.Invoke();

            if (_value == 0)
                BecameZero?.Invoke();
        }
    }

    public void Reset()
    {
        Value = Max;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new IndexOutOfRangeException(nameof(damage));

        Value -= damage;
    }

    public void TakeHealth(int health)
    {
        if (health < 0)
            throw new IndexOutOfRangeException(nameof(health));

        Value += health;
    }

    public void Zeroize()
    {
        Value = 0;
    }
}