using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour, IPooledObject<Tile>
{
    [SerializeField] private Transform _anchor;
    [SerializeField] private float _flightHeight = 5f; // temp: Вынести это и поле ниже в отдельный конфиг.
    [SerializeField] private float _flightDuration = 1f;
    
    private SpriteRenderer _spriteRenderer;

    private FlightAnimation _flightAnimation = new FlightAnimation();

    public event Action<Tile> Released;

    public Bounds Bounds => _spriteRenderer.bounds;
    public Vector3 AttachPosition => _anchor.position;
    public TileConfig Config { get; private set; }

    public void Initialize(TileConfig tileConfig)
    {
        Config = tileConfig;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Config.Color;
    }

    public void Release()
    {
        Released?.Invoke(this);
    }

    public void StackTo(Transform parent, Vector3 attachPosition)
    {
        transform.SetParent(parent); 
        transform.position = attachPosition;
    }

    public void FlyTo(Transform parent, Vector3 attachPosition)
    {
        transform.SetParent(parent);
        _flightAnimation.Fly(transform, attachPosition, _flightHeight, _flightDuration);
    }
}