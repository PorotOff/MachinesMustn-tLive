using UnityEngine;

[CreateAssetMenu(fileName = "CurrencyConfig", menuName = "Configurations/NonGameplay/CurrencyConfig", order = 0)]
public class CurrencyConfig : ScriptableObject
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
}