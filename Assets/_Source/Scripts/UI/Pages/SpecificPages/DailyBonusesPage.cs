using UnityEngine;
using UnityEngine.UI;

public class DailyBonusesPage : Page
{
    [SerializeField] private Button _back;

    private void OnEnable()
    {
        _back.onClick.AddListener(OpenMainMenuPage);
    }

    private void OnDisable()
    {
        _back.onClick.RemoveListener(OpenMainMenuPage);
    }

    private void OpenMainMenuPage()
    {
        PagesService.OpenPage<MainMenuPage>();
    }
}