using System.Collections.Generic;
using UnityEngine;

public class CombatUnitRoot : MonoBehaviour
{
    [field: SerializeField] public Transform ViewContainer { get; private set; }
    [field: SerializeField] public HealthDisplayerAtBar HealthDisplayerAtBar { get; private set; }
    [field: SerializeField] public List<CombatUnitStatsDisplayerAtBar> StatsDisplayersAtBar { get; private set; } = new List<CombatUnitStatsDisplayerAtBar>();
}