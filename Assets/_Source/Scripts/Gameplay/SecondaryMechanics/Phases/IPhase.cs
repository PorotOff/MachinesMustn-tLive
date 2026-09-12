using System;

public interface IPhase
{
    public event Action Over;

    public void Enter();
}