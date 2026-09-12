public class AttackEnergyStat : Stat, IIndicateable
{
    public AttackEnergyStat(int energyStripeCapacity, int energyStripesCount, int min, int max, int current) : base(min, max, current)
    {
        EnergyStripeCapacity = energyStripeCapacity;
        EnergyStripesCount = energyStripesCount;
    }

    public int EnergyStripeCapacity { get; private set; }
    public int EnergyStripesCount { get; private set; }
    public int AllowedAttacksCount => Current / EnergyStripeCapacity;
}