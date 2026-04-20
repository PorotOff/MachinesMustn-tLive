using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour, IPooledObject<Tile>
{
    [SerializeField] private Transform _anchor;

    private Transform _transform;
    private SpriteRenderer _spriteRenderer;

    public event Action<Tile> Released;

    public Bounds Bounds => _spriteRenderer.bounds;
    public Vector3 AttachPosition => _anchor.position;
    public TileConfig Config { get; private set; }

    public void Initialize(TileConfig tileConfig)
    {
        Config = tileConfig;

        _transform = transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        ApplyConfig();
    }

    public void Release()
    {
        Released?.Invoke(this);
    }

    public void StackTo(Transform parent, Vector3 attachPosition)
    {
        transform.SetParent(parent);
        _transform.position = attachPosition;
    }

    private void ApplyConfig()
    {
        _spriteRenderer.color = Config.Color;
    }
}