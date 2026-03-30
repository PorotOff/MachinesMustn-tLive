using System.Collections.Generic;
using UnityEngine;

public class BoxColliderCalculator
{
    private List<Bounds> _childsBounds;
    private Transform _parentTransform;

    public BoxColliderCalculator(List<Bounds> childsBounds, Transform parentTransform)
    {
        _childsBounds = childsBounds;
        _parentTransform = parentTransform;
    }

    public Vector2 CalculateBoxColliderLocalSize()
    {
        Bounds worldBounds = CalculateParentBounds();
        Vector3 localMin = _parentTransform.InverseTransformPoint(worldBounds.min);
        Vector3 localMax = _parentTransform.InverseTransformPoint(worldBounds.max);

        return new Vector2(localMax.x - localMin.x, localMax.y - localMin.y);
    }

    public Vector2 CalculateBoxColliderLocalCenter()
    {
        Bounds worldBounds = CalculateParentBounds();
        Vector3 localMin = _parentTransform.InverseTransformPoint(worldBounds.min);
        Vector3 localMax = _parentTransform.InverseTransformPoint(worldBounds.max);
        int devider = 2;

        return new Vector2((localMax.x + localMin.x) / devider, (localMax.y + localMin.y) / devider);
    }

    private Bounds CalculateParentBounds()
    {
        Bounds parentBounds = _childsBounds[0];
        int childBoundsIndex = 1;

        if (_childsBounds.Count == childBoundsIndex)
            return parentBounds;

        for (int i = childBoundsIndex; i < _childsBounds.Count; i++)
        {
            parentBounds.Encapsulate(_childsBounds[i]);
        }

        return parentBounds;
    }
}