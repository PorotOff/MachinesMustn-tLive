using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragger : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField, Min(0)] private float _rayDistance = 15f;
    [SerializeField, Min(0)] private int _maxHitsCount = 16;

    private Transform _transform;

    public event Action PickedUp;
    public event Action<IAttachmentPoint> PuttedDown;

    private RaycastComponentDetector<IAttachmentPoint> _attachmentPointDetector;
    private bool _pickedUp;
    private Vector2 _pointerPosition;

    private void Update()
    {
        if (_pickedUp)
        {
            MoveAtPointerPosition(_pointerPosition);
        }
    }

    public void Initialize(Transform transform)
    {
        _transform = transform;
        _attachmentPointDetector = new RaycastComponentDetector<IAttachmentPoint>(_rayDistance, _maxHitsCount, _transform);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        MoveAtPointerPosition(eventData.position);
        PickedUp?.Invoke();

        _pointerPosition = eventData.position;
        _pickedUp = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _attachmentPointDetector.TryDetect(out IAttachmentPoint attachmentPoint);
        PuttedDown?.Invoke(attachmentPoint);

        _pickedUp = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _pointerPosition = eventData.position;
    }

    private void MoveAtPointerPosition(Vector2 pointerPosition)
    {
        Vector3 dragPosition = Camera.main.ScreenToWorldPoint(pointerPosition);
        dragPosition.z = _transform.position.z;

        _transform.position = Vector3.Lerp(_transform.position, dragPosition, Constants.DragInterpolationStep);
    }
}