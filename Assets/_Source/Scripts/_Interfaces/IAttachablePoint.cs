public interface IAttachablePoint
{
    public bool IsFree { get; }

    public void Occupy(IAttachable attachable);
    public void Release();
}