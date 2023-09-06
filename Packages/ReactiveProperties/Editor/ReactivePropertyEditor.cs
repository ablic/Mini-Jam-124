using UnityEditor;

namespace ReactiveProperties.Editor
{
    [CustomEditor(typeof(ReactivePropertyBase))]
    public abstract class ReactivePropertyEditor : UnityEditor.Editor
    {
        public virtual void ValueField(string label = "Value")
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("value"));
        }
    }
}