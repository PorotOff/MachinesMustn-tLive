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

    private TilesStack _tilesStack;
    private BoxColliderChanger _boxColliderChanger;

    private IAttachablePoint _attachmentPoint;

    public event Action<Pillar> Released;

    public IReadOnlyTilesStack TilesStack => _tilesStack;

    public void Initialize(List<Tile> tiles)
    {
        _transform = transform;
        _boxCollider = GetComponent<BoxCollider2D>();
        _dragger = GetComponent<Dragger>();

        _tilesStack = new TilesStack(tiles);
        _boxColliderChanger = new BoxColliderChanger(_boxCollider, _transform);

        _dragger.Initialize(_transform);

        _boxColliderChanger.SetScaleAndOffset(_tilesStack.GeneralBounds);

        _dragger.PuttedDownAboveAttachablePoint += Attach;
        _dragger.JustPuttedDown += Return;
        _tilesStack.TilesOver += Detach;  
    }

    public void AddTile(Tile tile)
    {
        tile.StackTo(_transform, _tilesStack.Peek().AttachPosition);
        _tilesStack.Add(tile);
    }

    public Tile PopTile()
    {
        Tile topTile = _tilesStack.Pop();

        return topTile;
    }

    public void Attach(IAttachablePoint attachmentPoint)
    {
        if (attachmentPoint.IsFree == false)
            return;

        _attachmentPoint = attachmentPoint;
        _attachmentPoint.Occupy(this);
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
        _dragger.PuttedDownAboveAttachablePoint -= Attach;
        _dragger.JustPuttedDown -= Return;
        _tilesStack.TilesOver -= Detach;  

        Released?.Invoke(this);
    }
}