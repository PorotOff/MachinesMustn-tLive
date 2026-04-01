using System;
using UnityEngine;

public class Cell : MonoBehaviour, IAttachablePoint
{
    [SerializeField] private Transform _attachPoint;

    public event Action<IAttachable> Attached;
    public event Action Detached;

    public IAttachable Attachable { get; private set; }
    public bool IsFree => Attachable == null;

    public void Occupy(IAttachable attachable)
    {
        if (IsFree == false)
            return;

        Attachable = attachable;
        Attachable.Attach(_attachPoint.position);

        Debug.Log($"{name}: Attached");
        Attached?.Invoke(attachable);
    }

    public void Release()
    {
        Attachable = null;
        Detached?.Invoke();
    }
}