using UnityEngine;

public class RaycastComponentDetector<T> where T : class
{
    private float _rayDistance = 15f;
    private Transform _transform;

    private RaycastHit2D[] _raycastHits;

    public RaycastComponentDetector(float rayDistance, int raycastHitsCount, Transform transform)
    {
        _rayDistance = rayDistance;
        _transform = transform;

        _raycastHits = new RaycastHit2D[raycastHitsCount];
    }

    public bool TryDetect(out T component)
    {
        component = null;

        Vector2 origin = _transform.position;
        Vector2 direction = _transform.forward;        
        Physics2D.RaycastNonAlloc(origin, direction, _raycastHits, _rayDistance);
        
        foreach (var hit in _raycastHits)
        {
            if (hit.collider.TryGetComponent(out component))
                return true;
        }

        return false;
    }
}