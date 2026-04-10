using System;
using UnityEngine;

public class Health : IDisplayableAtBar
{
    private int _current;

    public Health()
    {
        Reset();
    }

    public event Action Changed;
    public event Action BecameZero;

    public int Max => 100;
    public int Current
    {
        get => _current;

        private set
        {
            _current = Mathf.Clamp(value, 0, Max);
            Changed?.Invoke();

            if (_current == 0)
                BecameZero?.Invoke();
        }
    }

    public void Reset()
    {
        Current = Max;
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            throw new IndexOutOfRangeException(nameof(damage));

        Current -= damage;
    }

    public void TakeHealth(int health)
    {
        if (health < 0)
            throw new IndexOutOfRangeException(nameof(health));

        Current += health;
    }

    public void Zeroize()
    {
        Current = 0;
    }
}