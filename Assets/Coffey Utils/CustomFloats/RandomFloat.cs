using UnityEngine;

public static class RandomFloat
{
    public static float MinMax(Vector2 minMax)
    {
        return minMax.y > minMax.x ? Random.Range(minMax.x, minMax.y) : minMax.x;
    }
}