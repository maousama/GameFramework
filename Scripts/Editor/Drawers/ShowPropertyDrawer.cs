using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomPropertyDrawer(typeof(ShowPropertyAttribute))]
public class ShowPropertyDrawer : PropertyDrawer
{
    private PropertyInfo propertyInfo = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        object targetObj = property.serializedObject.targetObject;
        object oldValue = fieldInfo.GetValue(targetObj);
        ShowPropertyAttribute attr = attribute as ShowPropertyAttribute;
        EditorGUI.BeginChangeCheck();
        if (attr.MinValue != null && attr.MaxValue != null)
        {
            if (property.propertyType == SerializedPropertyType.Float)
                EditorGUI.Slider(position, property, attr.MinValue.Value, attr.MaxValue.Value, label);
            else if (property.propertyType == SerializedPropertyType.Integer)
                EditorGUI.IntSlider(position, property, (int)attr.MinValue, (int)attr.MaxValue, label);
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }

        // Change property value
        if (EditorGUI.EndChangeCheck())
        {
            property.serializedObject.ApplyModifiedProperties();
            Type targetObjType = targetObj.GetType();
            if (propertyInfo == null)
            {
                propertyInfo = GetPropertyInfo(attr, property, targetObjType);

                //Check property
                if (propertyInfo == null)
                {
                    Debug.LogError("Invalid property name: " + attr.PropertyName + "\nCheck your [SetProperty] attribute");
                }
            }
            else
            {
                Debug.Log(oldValue);
                object newValue = fieldInfo.GetValue(targetObj);
                Debug.Log(newValue);
                fieldInfo.SetValue(targetObj, oldValue);
                propertyInfo.SetValue(targetObj, newValue, null);
            }
        }
    }

    private PropertyInfo GetPropertyInfo(ShowPropertyAttribute setPropertyAttribute, SerializedProperty property, Type type)
    {
        PropertyInfo propertyInfo;
        //Obtaining property names based on parameters
        string propertyName = null;
        if (!string.IsNullOrEmpty(setPropertyAttribute.PropertyName))
        {
            propertyName = setPropertyAttribute.PropertyName;
        }
        else
        {
            char[] chars = property.name.ToCharArray();
            chars[0] = char.ToUpper(chars[0]);
            propertyName = new string(chars);
        }
        //Get property
        propertyInfo = type.GetProperty(propertyName);
        return propertyInfo;
    }
}
