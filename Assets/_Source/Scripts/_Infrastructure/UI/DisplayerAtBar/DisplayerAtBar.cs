using System;
using UnityEngine;

public abstract class DisplayerAtBar : MonoBehaviour
{
    [SerializeField] private MinToMaxValueIndicator _indicator;

    protected IDisplayableAtBar Displayable;

    public Type DisplayableType => Displayable.GetType();

    public void Initialize(IDisplayableAtBar displayeable)
    {
        Displayable = displayeable;
        _indicator.Initialize(0, Displayable.Max, Displayable.Current);
        Display();
    }

    public void Subscribe()
    {
        Displayable.Changed += Display;
    }

    public void Unsubscribe()
    {
        Displayable.Changed -= Display;
    }

    protected virtual void Display()
    {
        _indicator.Display(Displayable.Current);
    }
}