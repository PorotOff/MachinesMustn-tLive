using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private List<ShopSlotConfig> _slots;
    
    public void Buy(ShopSlotConfig slot)
    {
        Currency currency = _wallet.GetCurrency(slot.Currency.ID);

        if (currency.CanRemove(slot.Cost))
        {
            slot.Buy();
        }
        else
        {
            Debug.Log($"Недостаточно денег. ({currency.Current}/{slot.Cost})");
        }
    }
}