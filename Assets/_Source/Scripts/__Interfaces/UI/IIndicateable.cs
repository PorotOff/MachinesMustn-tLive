using System;

public interface IIndicateable
{
    public event Action Changed;

    public int Max { get; }
    public int Min { get; }
    public int Current { get; }
}