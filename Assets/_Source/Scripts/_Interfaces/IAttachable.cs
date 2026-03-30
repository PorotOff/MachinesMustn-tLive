using UnityEngine;

public interface IAttachable
{
    public void Attach(IAttachablePoint attachmentPoint);
    public void Attach(Vector3 position);
    public void Detach();
}