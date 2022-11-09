using System;

namespace CustomFloats
{
    [Serializable]
    public struct RangedFloat
    {
        public float MinValue;
        public float MaxValue;

        public RangedFloat(float min, float max)
        {
            MinValue = min;
            MaxValue = max;
        }

        public float GetRandom()
        {
            return UnityEngine.Random.Range(MinValue, MaxValue);
        }
    }
}