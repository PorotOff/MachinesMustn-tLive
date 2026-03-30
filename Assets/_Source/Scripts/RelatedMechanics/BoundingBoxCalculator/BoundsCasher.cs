using UnityEngine;

public class BoundsCasher
{
    public BoundsCasher(SpriteRenderer spriteRenderer)
    {
        Bounds = spriteRenderer.bounds;
    }

    public Bounds Bounds { get; }
}