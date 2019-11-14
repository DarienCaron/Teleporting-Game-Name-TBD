using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableVariable<T> : ScriptableObject
{

    public T Value;

    public T GetValue()
    {
        return Value;
    }

    public void SetValue(T val)
    {
        Value = val;
    }
}
