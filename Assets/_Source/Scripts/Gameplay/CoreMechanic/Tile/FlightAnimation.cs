using DG.Tweening;
using UnityEngine;

public class FlightAnimation
{
    public void Fly(Transform transform, Vector3 targetPosition, float height, float duration)
    {
        int jumpsCount = 1;
        transform.DOJump(targetPosition, height, jumpsCount, duration);
    }
}