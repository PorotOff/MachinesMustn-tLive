using UnityEngine;

public abstract class DisplayerAtBar<T> : MonoBehaviour where T : IDisplayableAtBar
{
    [SerializeField] private MinToMaxValueIndicator _indicator;

    protected T Displayeable;

    public void Initialize(T displayeable)
    {
        Displayeable = displayeable;
        _indicator.Initialize(0, Displayeable.Max, Displayeable.Current);
        Display();
    }

    public void Subscribe()
    {
        Displayeable.Changed += Display;
    }

    public void Unsubscribe()
    {
        Displayeable.Changed -= Display;
    }

    protected virtual void Display()
    {
        _indicator.Display(Displayeable.Current);
    }
}