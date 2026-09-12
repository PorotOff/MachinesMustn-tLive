using System;
using UnityEngine;

public abstract class Cell : MonoBehaviour, IAttachablePoint
{
    [SerializeField] private Transform _attachPoint;

    public event Action<IAttachable> Occupied;
    public event Action Detached;

    public IAttachable Attachable { get; private set; }
    public bool IsFree => Attachable == null;
    public bool IsEnableCollider => GetIsEnableCollider();

    public void Occupy(IAttachable attachable)
    {
        if (attachable != Attachable && IsFree == false)
            return;

        Attachable = attachable;
        Attachable.Attach(_attachPoint.position);

        Occupied?.Invoke(attachable);
    }

    public void Release()
    {
        Attachable = null;
        Detached?.Invoke();
    }

    public void Clear()
    {
        if (Attachable is Pillar pillar)
        {
            pillar.Release();
        }

        Attachable = null;
    }

    protected abstract bool GetIsEnableCollider();
}