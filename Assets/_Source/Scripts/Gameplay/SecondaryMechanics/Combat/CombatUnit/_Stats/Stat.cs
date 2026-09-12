using System;
using UnityEngine;

public abstract class Stat
{
    public Stat(int min, int max, int current)
    {
        if (min < 0)
            throw new ArgumentOutOfRangeException(nameof(min));

        if (max < 0)
            throw new ArgumentOutOfRangeException(nameof(max));

        if (current < 0)
            throw new ArgumentOutOfRangeException(nameof(current));

        if (max < min)
            throw new InvalidOperationException($"{nameof(max)} less than {nameof(min)}");

        if (current < min)
            throw new InvalidOperationException($"{nameof(current)} less than {nameof(min)}");

        if (current > max)
            throw new InvalidOperationException($"{nameof(current)} greater than {nameof(max)}");

        Min = min;
        Max = max;
        Current = current;
        
        Changed?.Invoke();
    }

    public event Action Changed;

    public int Min { get; }
    public int Max { get; }
    public int Current { get; private set; }

    public void Increase(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        Current = Mathf.Min(Max, Current + amount);
        Changed?.Invoke();
    }

    public void Reduce(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        Current = Mathf.Max(Min, Current - amount);
        Changed?.Invoke();
    }

    public void Reset()
    {
        Current = Max;
        Changed?.Invoke();
    }

    public void Zeroize()
    {
        Current = 0;
        Changed?.Invoke();
    }
}