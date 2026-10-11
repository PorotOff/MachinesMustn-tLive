using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Dragger))]
public class Pillar : MonoBehaviour, IPooledObject<Pillar>, IAttachable
{
    [SerializeField] private Transform _tilesContainer;
    [SerializeField] private TMP_Text _tilesCounter;

    private Transform _transform;
    private BoxCollider2D _boxCollider;
    private Dragger _dragger;

    private TileStack _tileStack;
    private BoxColliderTransformer _boxColliderTransformer;

    private IAttachmentPoint _attachmentPoint;

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

        UpdateTilesCounter(_tileStack.Peek().AttachPosition);

        Subscribe();
    }

    public void AddTile(Tile tile)
    {
        Vector3 newAttachPosition = _tileStack.Peek().AttachPosition;
        
        tile.StackTo(_tilesContainer, newAttachPosition);
        _tileStack.Add(tile);
        
        UpdateTilesCounter(newAttachPosition);
    }

    public Tile PopTile()
    {
        Tile topTile = _tileStack.Pop();
        return topTile;
    }

    public void Attach(IAttachmentPoint attachmentPoint)
    {
        if (attachmentPoint.IsFree == false)
        {
            _attachmentPoint.ReturnAttachable();
            return;
        }

        Detach();

        _attachmentPoint = attachmentPoint;
        _boxCollider.enabled = _attachmentPoint.IsEnableCollider;        

        _attachmentPoint.Occupy(this);
    }

    public void Attach(Vector3 position)
    {
        _transform.position = position;
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
        if (_tileStack.Count > 0)
        {
            _tileStack.Clear();
        }
        
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

    private void OnPuttedDown(IAttachmentPoint attachablePoint)
    {
        if (attachablePoint == null)
        {
            _attachmentPoint.ReturnAttachable();
            return;
        }
        
        Attach(attachablePoint);
    }

    private void UpdateTilesCounter(Vector3 newPosition)
    {
        _tilesCounter.text = $"{_tileStack.Count}";
        _tilesCounter.transform.position = newPosition;
    }
}   