using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RequiredAttribute))]
public class RequiredDrawer : PropertyDrawer {
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    EditorGUI.PropertyField(position, property, label);

    if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null) {
      position.y += 18;
      position.height = 40; // Adjust this value based on your preference for the height of the help box
      position.width = 150;
      EditorGUI.HelpBox(position, "This field is required!", MessageType.Error);
    }
  }

  public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
    if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null)
      return base.GetPropertyHeight(property, label) +
             45; // Adjust this value based on the added height you set for the help box

    return base.GetPropertyHeight(property, label);
  }
}