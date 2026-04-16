using UnityEngine;

public abstract class Page : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    [SerializeField] protected PageService PageService;

    public void Open()
    {
        _canvasGroup.alpha = 1;
    }

    public void Close()
    {
        _canvasGroup.alpha = 0;
    }
}