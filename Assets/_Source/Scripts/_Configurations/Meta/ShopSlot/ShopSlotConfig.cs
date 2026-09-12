using UnityEngine;

[CreateAssetMenu(fileName = "ShopSlotConfig", menuName = "Configurations/NonGameplay/ShopSlotConfig", order = 0)]
public class ShopSlotConfig : ScriptableObject
{
    [Tooltip("Must implement IPurchasable")]
    [field: SerializeField] public MonoBehaviour Purchasable { get; private set; }
    [field: SerializeField] public CurrencyConfig Currency { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public bool IsPurchased { get; private set; }

    private void OnValidate()
    {
        ValidatePurchasable();
    }

    public void Buy()
    {
        IsPurchased = true;
    }

    private void ValidatePurchasable()
    {
        if (Purchasable == null)
            return;
            
        if (Purchasable is not IPurchasable)
        {
            Debug.LogWarning($"Your {nameof(Purchasable)} not implemented IPurchasable interface");
            Purchasable = null;
        }
    }
}