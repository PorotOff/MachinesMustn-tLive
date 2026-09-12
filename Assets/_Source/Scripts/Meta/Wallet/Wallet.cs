using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    [field: SerializeField] public List<CurrencyConfig> _currencyConfigs { get; private set; }

    private List<Currency> _currencies = new List<Currency>();

    private void Awake()
    {
        _currencies = CreateCurrencies(_currencyConfigs);
    }

    public Currency GetCurrency(int iD)
    {
        Currency currency = _currencies.FirstOrDefault(currency => currency.Config.ID == iD);

        if (currency == null)
            throw new ArgumentNullException();

        return currency;
    }

    private List<Currency> CreateCurrencies(List<CurrencyConfig> currencyConfigs)
    {
        List<Currency> currencies = new List<Currency>();

        foreach (var currencyConfig in currencyConfigs)
        {
            Currency currency = new Currency(currencyConfig);
            currencies.Add(currency);
        }

        return currencies;
    }
}