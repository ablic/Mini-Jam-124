using UnityEditor;

namespace ReactiveProperties.Editor
{
    [CustomEditor(typeof(IntegerProperty))]
    public class IntegerPropertyEditor : ReactivePropertyEditor
    {
        public override void OnInspectorGUI()
        {
            SerializedProperty minProperty = serializedObject.FindProperty("min");
            SerializedProperty maxProperty = serializedObject.FindProperty("max");
            SerializedProperty valueProperty = serializedObject.FindProperty("value");

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("Range", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(minProperty);
            EditorGUILayout.PropertyField(maxProperty);

            if (minProperty.intValue > maxProperty.intValue)
                minProperty.intValue = maxProperty.intValue;
            
            EditorGUILayout.Space(20f);

            ValueField();

            if (EditorGUI.EndChangeCheck())
                serializedObject.ApplyModifiedProperties(); 
        }

        public override void ValueField(string label = "Value")
        {
            SerializedProperty minProperty = serializedObject.FindProperty("min");
            SerializedProperty maxProperty = serializedObject.FindProperty("max");
            SerializedProperty valueProperty = serializedObject.FindProperty("value");

            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            valueProperty.intValue = EditorGUILayout.IntSlider(
                    valueProperty.intValue,
                    minProperty.intValue,
                    maxProperty.intValue);
        }
    }
}

