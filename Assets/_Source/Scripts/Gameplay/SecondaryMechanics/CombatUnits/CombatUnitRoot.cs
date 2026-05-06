using System;
using UnityEngine;

public class CombatUnitRoot : MonoBehaviour, IPooledObject<CombatUnitRoot>
{
    [SerializeField] private Transform _viewContainer;
    [SerializeField] private Canvas _canvas;

    public event Action<CombatUnitRoot> Released;

    public void Initialize(Camera worldCamera)
    {
        _canvas.renderMode = RenderMode.WorldSpace;
        _canvas.worldCamera = worldCamera;
    }

    public void Release()
    {
        Released?.Invoke(this);
    }
}