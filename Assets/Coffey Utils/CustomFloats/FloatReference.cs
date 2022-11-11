using System;

[Serializable]
public class FloatReference
{
    public bool UseConstant;
    public float ConstantValue;
    public FloatVariable Variable = null;

    public FloatReference()
    {
        UseConstant = true;
        ConstantValue = 0;
    }

    public FloatReference(float value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public float Value
    {
        get => UseConstant ? ConstantValue : Variable.Value;
        set
        {
            if (UseConstant) {
                ConstantValue = value;
            } else {
                Variable.Value = value;
            }
        }
    }

    public static implicit operator float(FloatReference reference)
    {
        return reference.Value;
    }
}