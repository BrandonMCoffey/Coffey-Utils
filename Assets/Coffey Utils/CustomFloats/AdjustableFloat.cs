using System;
using System.Collections;
using UnityEngine;

public enum ValueAdjustType
{
    AddRaw,
    AddBase,
    Multiply,
}

public class AdjustableFloat
{
    private float _baseValue;
    public float Value { get; private set; }
    public int ActivePositiveEffects { get; private set; }
    public int ActiveNegativeEffects { get; private set; }

    public event Action<int, int> ActiveEffects = delegate { };

    public void SetBaseValue(float baseValue)
    {
        _baseValue = baseValue;
        Value = baseValue;
    }

    public IEnumerator TemporaryIncrease(ValueAdjustType type, float amount, float timer)
    {
        ActivePositiveEffects++;
        IncreaseValue(type, amount);
        yield return new WaitForSecondsRealtime(timer);
        ActivePositiveEffects--;
        DecreaseValue(type, amount);
    }

    public IEnumerator TemporaryDecrease(ValueAdjustType type, float amount, float timer)
    {
        ActiveNegativeEffects++;
        DecreaseValue(type, amount);
        yield return new WaitForSecondsRealtime(timer);
        ActiveNegativeEffects--;
        IncreaseValue(type, amount);
    }

    public void IncreaseValue(ValueAdjustType type, float amount)
    {
        Value = type switch
        {
            ValueAdjustType.AddRaw   => Value + amount,
            ValueAdjustType.AddBase  => Value + _baseValue * amount,
            ValueAdjustType.Multiply => Value * amount,
            _                        => Value
        };
        UpdateEffects();
    }

    public void DecreaseValue(ValueAdjustType type, float amount)
    {
        Value = type switch
        {
            ValueAdjustType.AddRaw   => Value - amount,
            ValueAdjustType.AddBase  => Value - _baseValue * amount,
            ValueAdjustType.Multiply => Value / amount,
            _                        => Value
        };
        UpdateEffects();
    }

    private void UpdateEffects()
    {
        ActiveEffects?.Invoke(ActivePositiveEffects, ActiveNegativeEffects);
    }
}