public interface IReadOnlyAttackEnergy
{
    public void Add(int energyPoints);

    public void Remove(int energyPoints);

    public void FillFull();

    public void Clear();
}