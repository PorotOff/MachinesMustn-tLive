using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    // [SerializeField] private Transform _pivot;
    // [SerializeField] private Transform _attachPoint;

    private SpriteRenderer _spriteRenderer;

    public BoundsCasher BoundsCasher;

    public void Initialize()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        BoundsCasher = new BoundsCasher(_spriteRenderer);
    }
}