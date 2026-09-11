using TMPro;
using UnityEngine;

public class TextValueIndicator : ValueIndicator
{
    [SerializeField] private TextMeshProUGUI _text;      

    public override void Display(float current)
    {
        _text.text = $"{current}/{Indicateable.Max}";
    }

    public override void SetActive(bool isActive)
    {
        _text.gameObject.SetActive(isActive);
    }
}