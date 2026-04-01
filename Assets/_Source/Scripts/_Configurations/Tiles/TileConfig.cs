using UnityEngine;

[CreateAssetMenu(fileName = "TileConfig", menuName = "Configurations/TileConfig", order = 0)]
public class TileConfig : ScriptableObject
{
    [field: SerializeField] public int ID { get; private set; }
    [field: SerializeField] public Color Color { get; private set; }
}