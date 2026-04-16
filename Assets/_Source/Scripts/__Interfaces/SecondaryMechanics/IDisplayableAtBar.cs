using System;

public interface IDisplayableAtBar
{
    public event Action Changed;

    public int Max { get; }
    public int Current { get; }
}