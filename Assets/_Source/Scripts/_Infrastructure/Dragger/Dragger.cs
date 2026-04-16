using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField, Min(0)] private float _rayDistance = 15f;
    [SerializeField, Min(0)] private int _maxHitsCount = 16;

    private Transform _transform;

    public event Action PickedUp;
    public event Action<IAttachablePoint> PuttedDown;

    private RaycastComponentDetector<IAttachablePoint> _attachmentPointDetector;

    public void Initialize(Transform transform)
    {
        _transform = transform;
        _attachmentPointDetector = new RaycastComponentDetector<IAttachablePoint>(_rayDistance, _maxHitsCount, _transform);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MoveAtPointerPosition(eventData.position);
        PickedUp?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_attachmentPointDetector.TryDetect(out IAttachablePoint attachmentPoint) == false)
        {
            PuttedDown?.Invoke(null);
            return;
        }
        
        PuttedDown?.Invoke(attachmentPoint);
    }

    public void OnDrag(PointerEventData eventData)
    {
        MoveAtPointerPosition(eventData.position);
    }

    private void MoveAtPointerPosition(Vector2 pointerPosition)
    {
        Vector3 dragPosition = Camera.main.ScreenToWorldPoint(pointerPosition);
        dragPosition.z = _transform.position.z;

        _transform.position = dragPosition;     
    }

    // todo Прикрутить Lerp для плавного следования за курсором.
}