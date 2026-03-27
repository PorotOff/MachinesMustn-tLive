using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Transform _transform;

    public event Action PickedUp;

    private void Awake()
    {
        _transform = transform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Объект подобрали");
        _transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
        PickedUp?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Объект тащат");
        _transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
    }
}