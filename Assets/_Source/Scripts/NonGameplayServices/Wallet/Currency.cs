using System;
using UnityEngine;

public class Currency
{
    public Currency(CurrencyConfig config)
    {
        Config = config;
        Current = 0;
    }

    public event Action Changed;

    public CurrencyConfig Config { get; private set; }
    public int Current { get; private set; }

    public bool CanRemove(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        return Current - amount >= 0;
    }

    public void Add(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        Current = Mathf.Clamp(Current + amount, 0, int.MaxValue);

        Current += amount;
        Changed?.Invoke();
    }

    public void Remove(int amount)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (Current - amount < 0)
            throw new InvalidOperationException();

        Current -= amount;
        Changed?.Invoke();
    }
}