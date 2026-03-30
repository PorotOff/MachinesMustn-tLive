using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Dragger))]
public class Pillar : MonoBehaviour, IAttachable
{
    [SerializeField] private Transform _pivot;

    private BoxCollider2D _boxCollider;
    private Dragger _dragger;

    private Transform _transform;
    [SerializeField] private List<Tile> _tiles = new List<Tile>();
    private BoxColliderCalculator _boxColliderCalculator;

    private IAttachablePoint _attachmentPoint;

    // public Tile TopTile => _tiles.Peek();

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        _dragger = GetComponent<Dragger>();

        _transform = transform;

        _dragger.Initialize(_transform);

        #region For tests time
            foreach (var tile in _tiles)
            {
                tile.Initialize();
            }
            
            List<Bounds> childBounds = _tiles.Select(tile => tile.BoundsCasher.Bounds).ToList();
        #endregion

        _boxColliderCalculator = new BoxColliderCalculator(childBounds, _transform);
        
        _boxCollider.size = _boxColliderCalculator.CalculateBoxColliderLocalSize();
        _boxCollider.offset = _boxColliderCalculator.CalculateBoxColliderLocalCenter();
    }

    private void OnEnable()
    {
        _dragger.PuttedDown += Link;
        _dragger.PickedUp += Unlink;
    }

    private void OnDisable()
    {
        _dragger.PuttedDown -= Link;
        _dragger.PickedUp -= Unlink;
    }

    public void Link(IAttachablePoint attachmentPoint)
    {
        _attachmentPoint = attachmentPoint;
        _attachmentPoint.Occupy(this);
    }

    public void Unlink()
    {
        _attachmentPoint.Release();
        _attachmentPoint = null;
    }

    public void Attach(IAttachablePoint attachmentPoint)
    {
        attachmentPoint.Occupy(this);
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
    }
}