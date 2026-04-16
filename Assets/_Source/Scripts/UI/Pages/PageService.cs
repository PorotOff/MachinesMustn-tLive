using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PageService : MonoBehaviour
{
    [SerializeField] private List<Page> _pages;
    [SerializeField] private Page _mainPage;

    private void Start()
    {
        CloseAllPages();
        _mainPage.Open();
    }

    public void OpenPage<T>() where T : Page
    {
        Page page = _pages.FirstOrDefault(page => page.GetType() == typeof(T));

        if (page == null)
            throw new ArgumentNullException(nameof(page));

        CloseAllPages();
        page.Open();
    }

    private void CloseAllPages()
    {
        _pages.ForEach(page => page.Close());
    }
}