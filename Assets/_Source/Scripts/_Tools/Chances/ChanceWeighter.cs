using System.Collections.Generic;

public class ChanceWeighter
{
    private const int MaxChance = 100;
    private const int MinChance = 0;

    private int _maxChance;
    private int _minChance;

    public ChanceWeighter(int maxChance, int minChance)
    {
        _maxChance = maxChance;
        _minChance = minChance;
    }

    /// <summary>
    /// Метод линейно гденерирует шансы для значений и отдаёт словарь (шанс - значение).
    /// </summary>
    /// <param name="minDistributed">Мин определяемое (число, для которого хотим задать шанс)</param>
    /// <param name="maxDistributed">Макс определяемое (число, для которого хотим задать шанс)</param>
    /// <param name="minDeterminant">Мин определитель</param>
    /// <param name="maxDeterminant">Макс определитель</param>
    /// <param name="currentDeterminant">Текущий определитель (по какому числу будет определяться</param>
    /// <returns></returns>
    public Dictionary<int, int> GetChances(int minDistributed, int maxDistributed, int minDeterminant, int maxDeterminant, int currentDeterminant)
    {
        Dictionary<int, int> chancesWights = new Dictionary<int, int>();
        int currentDistributed = minDistributed;
        
        int firstChance = currentDeterminant.Remap(minDeterminant, maxDeterminant, _minChance, _maxChance);
        int lastChance = MaxChance - firstChance;

        int totalValuesCount = maxDistributed - minDistributed + 1;

        for (int i = currentDistributed; i <= maxDistributed; i++)
        {
            float lerpFactor = (float)(i - minDistributed) / (totalValuesCount - 1);
            int chance = (int)(firstChance + (lastChance - firstChance) * lerpFactor);

            chancesWights[chance] = i;
        }

        return chancesWights;
    }
}