using UnityEditor;

namespace ReactiveProperties.Editor
{
    [CustomEditor(typeof(FloatProperty))]
    public class FloatPropertyEditor : ReactivePropertyEditor
    {
        public override void OnInspectorGUI()
        {
            SerializedProperty minProperty = serializedObject.FindProperty("min");
            SerializedProperty maxProperty = serializedObject.FindProperty("max");

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.LabelField("Range", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(minProperty);
            EditorGUILayout.PropertyField(maxProperty);

            if (minProperty.floatValue > maxProperty.floatValue)
                minProperty.floatValue = maxProperty.floatValue;

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
            valueProperty.floatValue = EditorGUILayout.Slider(
                    valueProperty.floatValue,
                    minProperty.floatValue,
                    maxProperty.floatValue);
        }
    }
}

