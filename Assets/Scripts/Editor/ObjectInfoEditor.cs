using UnityEditor;
using Assets.Scripts.View.Common;
using System.Linq;
using Assets.Scripts.Core.Static;
using System;

public abstract class ObjectInfoEditor : Editor
{
    SerializedProperty mTypeProperty;
    SerializedProperty mPrefabProperty;
    Type[] mTypes;

    private void OnEnable()
    {
        mTypeProperty = serializedObject.FindProperty(nameof(ObjectInfo.ObjectType));
        mPrefabProperty = serializedObject.FindProperty(nameof(ObjectInfo.Prefab));
        mTypes = GetBaseObjectType().Assembly.GetTypes().Where(t => GetBaseObjectType().IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface).ToArray();
    }

    protected abstract Type GetBaseObjectType();

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var currentIndex = Array.FindIndex(mTypes, t => t.AssemblyQualifiedName == mTypeProperty.stringValue);
        var newIndex = EditorGUILayout.Popup(currentIndex, mTypes.Select(t => t.Name).ToArray());
        if (newIndex >= 0)
            mTypeProperty.stringValue = mTypes[newIndex].AssemblyQualifiedName;

        EditorGUILayout.PropertyField(mPrefabProperty);

        serializedObject.ApplyModifiedProperties();
    }
}
