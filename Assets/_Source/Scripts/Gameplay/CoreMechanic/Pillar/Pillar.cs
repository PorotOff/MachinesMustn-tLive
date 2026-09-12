using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Dragger))]
public class Pillar : MonoBehaviour, IPooledObject<Pillar>, IAttachable
{
    private Transform _transform;
    private BoxCollider2D _boxCollider;
    private Dragger _dragger;

    private TileStack _tileStack;
    private BoxColliderTransformer _boxColliderTransformer;

    private IAttachablePoint _attachmentPoint;

    public event Action<Pillar> Released;

    public IReadOnlyTileStack TileStack => _tileStack;

    private void Awake()
    {
        _transform = transform;
        _boxCollider = GetComponent<BoxCollider2D>();
        _dragger = GetComponent<Dragger>();
    }

    public void Initialize(List<Tile> tiles)
    {
        _tileStack = new TileStack(tiles);
        _boxColliderTransformer = new BoxColliderTransformer(_boxCollider, _transform);

        _dragger.Initialize(_transform);
        _boxColliderTransformer.SetScaleAndOffset(_tileStack.GeneralBounds);
        Subscribe();
    }

    public void AddTile(Tile tile)
    {
        tile.StackTo(_transform, _tileStack.Peek().AttachPosition);
        _tileStack.Add(tile);
    }

    public Tile PopTile()
    {
        Tile topTile = _tileStack.Pop();
        return topTile;
    }

    public void Attach(IAttachablePoint attachmentPoint)
    {
        if (attachmentPoint.IsFree == false)
        {
            Return();
            return;
        }

        Detach();

        attachmentPoint.Occupy(this);
        _boxCollider.enabled = attachmentPoint.IsEnableCollider;
        _attachmentPoint = attachmentPoint;
    }

    public void Attach(Vector3 position)
    {
        _transform.position = position;
    }

    public void Return()
    {
        _attachmentPoint.Occupy(this);
    }

    public void Detach()
    {
        if (_attachmentPoint == null)
            return;

        _attachmentPoint.Release();
        _attachmentPoint = null;
    }

    public void Release()
    {
        Detach();
        Unsubscribe();
        Released?.Invoke(this);
    }

    private void Subscribe()
    {
        _dragger.PuttedDown += OnPuttedDown;
        _tileStack.TilesOver += Release;
    }

    private void Unsubscribe()
    {
        _dragger.PuttedDown -= OnPuttedDown;
        _tileStack.TilesOver -= Release;
    }

    private void OnPuttedDown(IAttachablePoint attachablePoint)
    {
        if (attachablePoint == null)
        {
            Return();
        }
        else
        {
            Attach(attachablePoint);
        }
    }
}