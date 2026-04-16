using UnityEngine;
using UnityEngine.UI;

public class MainMenuPage : Page
{
    [SerializeField] private Button _dailyBonuses;

    private void OnEnable()
    {
        _dailyBonuses.onClick.AddListener(OpenDailyBonusesPage);
    }

    private void OnDisable()
    {
        _dailyBonuses.onClick.RemoveListener(OpenDailyBonusesPage);
    }

    private void OpenDailyBonusesPage()
    {
        PageService.OpenPage<DailyBonusesPage>();
    }
}