using UnityEngine;

public class Cell : MonoBehaviour, IAttachablePoint
{
    [SerializeField] private Transform _attachPoint;

    private IAttachable _attachable;

    public bool IsFree => _attachable == null;

    public void Occupy(IAttachable attachable)
    {
        _attachable = attachable;
        _attachable.Attach(_attachPoint.position);
    }

    public void Release()
    {
        _attachable = null;
    }
}