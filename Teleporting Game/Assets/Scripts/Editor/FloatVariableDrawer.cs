using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

// Code referenced from Jason Weinmann
// Custom property drawer for float references.
[CustomPropertyDrawer(typeof(FloatReference))]
public class FloatVariableDrawer : PropertyDrawer
{

    public const string ConstantName = "UseConstant";
    public const string ConstantValueName = "ConstantValue";
    public const string VariableName = "Variable";
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        bool useConstant = property.FindPropertyRelative(ConstantName).boolValue;

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var rect = new Rect(position.position, Vector2.one * 30);

        if(EditorGUI.DropdownButton(rect, GetTexture(),FocusType.Keyboard, new GUIStyle() { fixedWidth = 50f, border = new RectOffset(1,1,1,1)}))
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Constant"),useConstant, () =>SetProperty(property,true));
            menu.AddItem(new GUIContent("Variable"), !useConstant, () => SetProperty(property, false));

            menu.ShowAsContext();


        }
        position.position += Vector2.right * 15;
        float value = property.FindPropertyRelative(ConstantValueName).floatValue;

        if(useConstant)
        {
            string newVal = EditorGUI.TextField(position, value.ToString());
            float.TryParse(newVal, out value);
            property.FindPropertyRelative(ConstantValueName).floatValue = value;
        }
        else
        {
            EditorGUI.ObjectField(position, property.FindPropertyRelative(VariableName), GUIContent.none);
        }
        
    }


    private GUIContent GetTexture()
    {
        return EditorGUIUtility.IconContent("LookDevPaneOption");
    }

    private void SetProperty(SerializedProperty property, bool val)
    {
        var prop = property.FindPropertyRelative(ConstantName);
        prop.boolValue = val;
        property.serializedObject.ApplyModifiedProperties();
    }

}
