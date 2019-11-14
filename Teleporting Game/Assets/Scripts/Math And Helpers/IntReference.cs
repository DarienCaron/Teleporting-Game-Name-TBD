using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class IntReference
{// Class for a float reference variable.
  
   
        public bool UseConstant = true;
        public int ConstantValue;
        public IntVariable Variable;

    public IntReference(int val, bool useConstant = true)
        {
            UseConstant = useConstant;
            if(UseConstant)
            {
                ConstantValue = val;
            }
            else
            {
                if(Variable)
                {
                    Variable.Value = val;
                }
                else
                {
                
                    Variable = ScriptableObject.CreateInstance<IntVariable>();
                    Variable.Value = val;
                }
            }
        }

        // function to simplify what value to return. If its constant, it will return the constant variable, otherwise it returns the floatvariables value.
        public float GetValue()
        {
            return UseConstant ? ConstantValue : Variable.Value;
        }

        public void SetValue(int val)
        {
            if (UseConstant)
            {
                ConstantValue = val;
            }
            else
            {
                Variable.Value = val;
            }
        }
    
}
