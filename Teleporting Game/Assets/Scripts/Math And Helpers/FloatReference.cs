﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class for a float reference variable.
[System.Serializable]
public class FloatReference 
{
    public bool UseConstant = true;
    public float ConstantValue;
    public FloatVariable Variable;

    // function to simplify what value to return. If its constant, it will return the constant variable, otherwise it returns the floatvariables value.
    public float GetValue()
    {
        return UseConstant ? ConstantValue : Variable.Value;
    }

    public void SetValue(float val)
    {
        if(UseConstant)
        {
            ConstantValue = val;
        }
        else
        {
            Variable.Value = val;
        }
    }
}
