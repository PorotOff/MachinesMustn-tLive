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
    private BoxColliderTransformer _boxColliderTransformer;

    private IAttachablePoint _attachmentPoint;

    public event Action<Pillar> Released;

    public IReadOnlyTilesStack TilesStack => _tilesStack;

    private void Awake()
    {
        _transform = transform;
        _boxCollider = GetComponent<BoxCollider2D>();
        _dragger = GetComponent<Dragger>();
    }

    public void Initialize(List<Tile> tiles)
    {
        _tilesStack = new TilesStack(tiles);
        _boxColliderTransformer = new BoxColliderTransformer(_boxCollider, _transform);

        _dragger.Initialize(_transform);
        _boxColliderTransformer.SetScaleAndOffset(_tilesStack.GeneralBounds);
        Subscribe();
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
        _tilesStack.TilesOver += Release;
    }

    private void Unsubscribe()
    {
        _dragger.PuttedDown -= OnPuttedDown;
        _tilesStack.TilesOver -= Release;
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