using UnityEngine;

public class CombatUnitRoot : MonoBehaviour
{
    [field: SerializeField] public Transform ViewContainer { get; private set; }
    [field: SerializeField] public HealthDisplayerAtBar HealthDisplayerAtBar { get; private set; }
}