using System;
using UnityEngine;

public abstract class Cell : MonoBehaviour, IAttachmentPoint
{
    [SerializeField] private Transform _attachPoint;

    public event Action<IAttachable> Occupied;
    public event Action Detached;

    public IAttachable Attachable { get; private set; } // todo это поле плохо инкапсулировано
    public bool IsFree => Attachable == null;
    public bool IsEnableCollider => GetIsEnableCollider(); // todo Переделать эту хуйню. Сейчас коллайдер столба
    // включается и отключается в зависимости от этого флага

    public void Occupy(IAttachable attachable)
    {
        if (attachable == null)
            throw new ArgumentNullException(nameof(attachable));

        if (IsFree == false)
            return;

        Attachable = attachable;
        Attachable.Attach(_attachPoint.position);

        Occupied?.Invoke(attachable);
    }

    public void ReturnAttachable()
    {
        if (Attachable == null)
            throw new ArgumentNullException(nameof(Attachable));

        Attachable.Attach(_attachPoint.position);
    }

    public void Release()
    {
        Attachable = null;
        Detached?.Invoke();
    }

    public void Clear()
    {
        if (Attachable == null)
            return;
            
        Attachable.Release();
    }

    protected abstract bool GetIsEnableCollider();
}