using TMPro;
using UnityEngine;

public class WavesView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _wavesCounterText;

    private string _template;

    private void Awake()
    {
        _template = _wavesCounterText.text;
    }

    public void Display(int remainingWaves, int totalWaves)
    {
        _wavesCounterText.SetText(_template, remainingWaves, totalWaves);
    }
}