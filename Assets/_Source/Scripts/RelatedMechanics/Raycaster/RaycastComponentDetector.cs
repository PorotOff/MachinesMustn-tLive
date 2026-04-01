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
        int hitsCount = Physics2D.RaycastNonAlloc(origin, direction, _raycastHits, _rayDistance);

        for (int i = 0; i < hitsCount; i++)
        {
            RaycastHit2D hit = _raycastHits[i];

            if (hit.collider == null)
                continue;

            if (hit.collider.TryGetComponent(out component))
                return true;
        }

        return false;
    }
}