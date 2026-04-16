using UnityEngine;
using UnityEngine.UI;

public class DailyBonusesPage : Page
{
    [SerializeField] private Button _back;

    private void OnEnable()
    {
        _back.onClick.AddListener(OpenDailyBonusesPage);
    }

    private void OnDisable()
    {
        _back.onClick.RemoveListener(OpenDailyBonusesPage);
    }

    private void OpenDailyBonusesPage()
    {
        PageService.OpenPage<DailyBonusesPage>();
    }
}