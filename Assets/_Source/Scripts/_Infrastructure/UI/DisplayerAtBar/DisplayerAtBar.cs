using System;

public class DisplayerAtBar
{
    private MinToMaxValueIndicator _indicator;
    private IDisplayableAtBar _displayable;

    public event Action Displayed;

    public DisplayerAtBar(MinToMaxValueIndicator indicator)
    {
        _indicator = indicator;
    }

    public void Initialize(IDisplayableAtBar displayeable)
    {
        _displayable = displayeable;
        _indicator.Initialize(0, _displayable.Max, _displayable.Current);
        Display();

        _displayable.Changed += Display;
        // todo Тоже пофиксить подписки
    }

    public void Subscribe()
    {
        
    }

    public void Unsubscribe()
    {
        _displayable.Changed -= Display;
    }

    protected virtual void Display()
    {
        _indicator.Display(_displayable.Current);
        Displayed?.Invoke();
    }
}