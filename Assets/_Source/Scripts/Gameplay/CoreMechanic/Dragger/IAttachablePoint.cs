public interface IAttachablePoint
{
    public bool IsFree { get; }
    public bool IsEnableCollider { get; }

    public void Occupy(IAttachable attachable);
    public void Release();
}