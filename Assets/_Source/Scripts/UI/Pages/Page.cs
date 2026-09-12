using UnityEngine;

public abstract class Page : MonoBehaviour
{
    [SerializeField] private RectTransform _pageContent;

    [SerializeField] protected PageService PagesService;

    public void Open()
    {
        _pageContent.gameObject.SetActive(true);
    }

    public void Close()
    {
        _pageContent.gameObject.SetActive(false);
    }
}