using System;

public class AttackEnergy
{
    public AttackEnergy(int energyStripeCapacity, int energyStripesCount)
    {
        EnergyStripeCapacity = energyStripeCapacity;
        EnergyStripesCount = energyStripesCount;
    }

    public event Action Changed;

    public int EnergyStripeCapacity { get; private set; }
    public int EnergyStripesCount { get; private set; }
    public int Current { get; private set; }
    public int Max => EnergyStripeCapacity * EnergyStripesCount;
    public int AllowedAttacksCount => Current / EnergyStripeCapacity;

    public void Add(int energyPoints)
    {
        Current += energyPoints;
        Changed?.Invoke();
    }

    public void Remove(int energyPoints)
    {
        Current -= energyPoints;
        Changed?.Invoke();
    }

    public void FillFull()
    {
        Current = Max;
        Changed?.Invoke();
    }

    public void Clear()
    {
        Current = 0;
        Changed?.Invoke();
    }
}