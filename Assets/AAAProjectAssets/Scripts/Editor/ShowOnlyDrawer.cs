using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
public class ShowOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        string valueStr;

        switch (prop.propertyType)
        {
            case SerializedPropertyType.Integer:
                valueStr = prop.intValue.ToString();
                break;
            case SerializedPropertyType.Boolean:
                valueStr = prop.boolValue.ToString();
                break;
            case SerializedPropertyType.Float:
                valueStr = prop.floatValue.ToString("0.00");
                break;
            case SerializedPropertyType.String:
                valueStr = prop.stringValue;
                break;
            case SerializedPropertyType.ObjectReference:
                if (prop.objectReferenceValue != null)
                {
                    valueStr = prop.objectReferenceValue.ToString();
                }
                else
                {
                    valueStr = "null";
                }
                break;
            case SerializedPropertyType.Vector2:
                valueStr = prop.vector2Value.ToString("0.0");
                break;
            case SerializedPropertyType.Vector3:
                valueStr = prop.vector3Value.ToString("0.0.0");
                break;
            case SerializedPropertyType.Enum:
                valueStr = prop.enumNames[prop.intValue];
                break;
            default:
                valueStr = "(not supported)";
                break;
        }

        EditorGUI.LabelField(position, label.text, valueStr);
    }
}