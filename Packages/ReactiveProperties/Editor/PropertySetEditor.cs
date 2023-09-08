using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ReactiveProperties.Editor
{
    [CustomEditor(typeof(PropertySet))]
    public class PropertySetEditor : UnityEditor.Editor
    {
        private Dictionary<ReactivePropertyBase, ReactivePropertyEditor> cachedEditors = new();
        private Dictionary<ReactivePropertyBase, SerializedObject> cachedSerializedObjects = new();

        private void Awake()
        {
            Undo.undoRedoPerformed += () =>
            {
                serializedObject.Update();

                foreach (var editor in cachedEditors.Values)
                    editor.serializedObject.Update();

                foreach (var obj in cachedSerializedObjects.Values)
                    obj.Update();
            };
        }

        public override void OnInspectorGUI()
        {
            SerializedProperty arrayProperty = serializedObject.FindProperty("<Properties>k__BackingField");

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(arrayProperty);

            for (int i = 0; i < arrayProperty.arraySize; i++)
            {
                SerializedProperty arrayElementProperty = arrayProperty.GetArrayElementAtIndex(i);

                if (arrayElementProperty.objectReferenceValue == null)
                    continue;

                EditorGUILayout.LabelField(new string('-', 100));
                ReactivePropertyBase reactiveProperty = (ReactivePropertyBase)arrayElementProperty.objectReferenceValue;
                UnityEditor.Editor editor = CreateEditor(reactiveProperty);

                if (editor is ReactivePropertyEditor reactiveEditor)
                {
                    if (!cachedEditors.ContainsKey(reactiveProperty))
                        cachedEditors.Add(reactiveProperty, reactiveEditor);

                    cachedEditors[reactiveProperty].ValueField(reactiveProperty.name);
                }
                else
                {
                    if (!cachedSerializedObjects.ContainsKey(reactiveProperty))
                        cachedSerializedObjects.Add(reactiveProperty, new SerializedObject(reactiveProperty));

                    EditorGUILayout.LabelField(reactiveProperty.name, EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(cachedSerializedObjects[reactiveProperty].FindProperty("value"), new GUIContent());
                }
            }

            EditorGUILayout.LabelField(new string('-', 100));

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();

                foreach (var editor in cachedEditors.Values)
                    editor.serializedObject.ApplyModifiedProperties();

                foreach (var obj in cachedSerializedObjects.Values)
                    obj.ApplyModifiedProperties();
            }

        }
    }
}