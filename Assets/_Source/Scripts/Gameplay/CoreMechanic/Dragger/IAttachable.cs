using UnityEngine;

public interface IAttachable
{
    public void Attach(IAttachmentPoint attachmentPoint);
    public void Attach(Vector3 position);
    public void Release();
}