using System.Collections.Generic;
using UnityEngine;

public class BoxColliderCalculator
{
    private Transform _parentTransform;

    public BoxColliderCalculator(Transform parentTransform)
    {
        _parentTransform = parentTransform;
    }

    public Vector2 CalculateBoxColliderLocalSize(List<Bounds> childsBounds)
    {
        Bounds worldBounds = CalculateParentBounds(childsBounds);
        Vector3 localMin = _parentTransform.InverseTransformPoint(worldBounds.min);
        Vector3 localMax = _parentTransform.InverseTransformPoint(worldBounds.max);

        return new Vector2(localMax.x - localMin.x, localMax.y - localMin.y);
    }

    public Vector2 CalculateBoxColliderLocalCenter(List<Bounds> childsBounds)
    {
        Bounds worldBounds = CalculateParentBounds(childsBounds);
        Vector3 localMin = _parentTransform.InverseTransformPoint(worldBounds.min);
        Vector3 localMax = _parentTransform.InverseTransformPoint(worldBounds.max);
        int devider = 2;

        return new Vector2((localMax.x + localMin.x) / devider, (localMax.y + localMin.y) / devider);
    }

    private Bounds CalculateParentBounds(List<Bounds> childsBounds)
    {
        Bounds parentBounds = childsBounds[0];
        int childBoundsIndex = 1;

        if (childsBounds.Count == childBoundsIndex)
            return parentBounds;

        for (int i = childBoundsIndex; i < childsBounds.Count; i++)
        {
            parentBounds.Encapsulate(childsBounds[i]);
        }

        return parentBounds;
    }
}