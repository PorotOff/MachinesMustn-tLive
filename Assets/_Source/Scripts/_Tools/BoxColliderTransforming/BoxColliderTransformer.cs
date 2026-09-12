using System.Collections.Generic;
using UnityEngine;

public class BoxColliderTransformer
{
    private BoxCollider2D _boxCollider;
    private BoxColliderCalculator _boxColliderCalculator;

    public BoxColliderTransformer(BoxCollider2D boxCollider, Transform transform)
    {
        _boxCollider = boxCollider;
        _boxColliderCalculator = new BoxColliderCalculator(transform);
    }

    public void SetScaleAndOffset(List<Bounds> childsBounds)
    {
        _boxCollider.size = _boxColliderCalculator.CalculateBoxColliderLocalSize(childsBounds);
        _boxCollider.offset = _boxColliderCalculator.CalculateBoxColliderLocalCenter(childsBounds);
    }
}