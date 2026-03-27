using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Transform _attachPoint;

    private IAttachable _attachable;

    public bool IsFree => _attachable == null;

    public void Occupy(IAttachable attachable)
    {
        _attachable = attachable;
    }

    public void Release()
    {
        _attachable = null;
    }
}