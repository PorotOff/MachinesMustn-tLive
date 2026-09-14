using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private PhaseService _phaseService;
    [SerializeField] private CombatUnitBuilder _combatUnitBuilder;
    [SerializeField] private WarriorChargingService _warriorChargingService;

    private void Awake()
    {
        _phaseService.Initialize();

        // _combatUnitBuilder.Build();

        // todo Я остановился на том, что мне нужно создать юнитов для _warriorChargingService
        // но перед этим мне нужно создать какой-то репозиторий воинов (типа которые купил игрок)
        // а с врагами пока поступить проще: просто спавнить одного балванчика

        // _warriorChargingService.Initialize();
    }

    private void OnEnable()
    {
        _phaseService.EnemiesDied += Win;
        _phaseService.WarriorsDied += Lose;
    }

    private void OnDisable()
    {
        _phaseService.EnemiesDied -= Win;
        _phaseService.WarriorsDied -= Lose;
    }

    private void Win()
    {
        Debug.Log("Победа");
    }

    private void Lose()
    {
        Debug.Log("Поражение");
    }
}