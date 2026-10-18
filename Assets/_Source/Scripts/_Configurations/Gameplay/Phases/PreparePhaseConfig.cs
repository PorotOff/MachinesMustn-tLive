using UnityEngine;

[CreateAssetMenu(fileName = "PreparePhaseConfig", menuName = "Configurations/Gameplay/PreparePhaseConfig", order = 0)]
public class PreparePhaseConfig : ScriptableObject
{
    [field: SerializeField] public int AvailableSteps { get; private set; } = 9;
}