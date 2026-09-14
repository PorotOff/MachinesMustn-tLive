public interface IAttachmentPoint // todo заменить интерфейс на абстрактный класс?
{
    public bool IsFree { get; }
    public bool IsEnableCollider { get; }

    public void Occupy(IAttachable attachable);
    public void ReturnAttachable();
    public void Release();
}