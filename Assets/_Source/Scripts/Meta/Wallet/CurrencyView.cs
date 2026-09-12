using TMPro;
using UnityEngine;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _iconRenderer;
    [SerializeField] private TextMeshProUGUI _currencyCounter;

    public void Initialize(Sprite icon)
    {
        _iconRenderer.sprite = icon;
    }

    public void Display(int amount)
    {
        _currencyCounter.text = amount.ToString();
    }
}