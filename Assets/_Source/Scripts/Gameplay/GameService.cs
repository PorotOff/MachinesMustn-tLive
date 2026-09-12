using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private PhaseService _phaseService;

    private void Awake()
    {
        _phaseService.Initialize();
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