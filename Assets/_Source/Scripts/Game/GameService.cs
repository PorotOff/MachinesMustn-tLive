using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private PhasesService _phasesService;

    private void Awake()
    {
        _phasesService.Initialize();
    }

    private void OnEnable()
    {
        _phasesService.EnemiesDied += Win;
        _phasesService.WarriorsDied += Lose;
    }

    private void OnDisable()
    {
        _phasesService.EnemiesDied -= Win;
        _phasesService.WarriorsDied -= Lose;
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