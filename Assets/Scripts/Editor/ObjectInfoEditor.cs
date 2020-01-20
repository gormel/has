using UnityEditor;
using Assets.Scripts.View.Common;
using System.Linq;
using Assets.Scripts.Core.Static;
using System;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public abstract class ObjectInfoEditor : Editor
{
    private SerializedProperty mPairsProperty;
    private string[] mTypes;

    private void OnEnable()
    {
        mPairsProperty = serializedObject.FindProperty(nameof(PrefabDatabase.Pairs));
        mTypes = GetBaseObjectType().Assembly.GetTypes()
            .Where(t => GetBaseObjectType().IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
            .Select(t => t.AssemblyQualifiedName)
            .ToArray();
    }


    protected abstract Type GetBaseObjectType();

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var unUsedTypes = new List<string>(mTypes);
        for (int i = 0; i < mPairsProperty.arraySize; i++)
        {
            var elem = mPairsProperty.GetArrayElementAtIndex(i);
            var typeProperty = elem.FindPropertyRelative(nameof(PrefabDatabasePair.ObjectType));
            var refProperty = elem.FindPropertyRelative(nameof(PrefabDatabasePair.Prefab));

            if (typeProperty == null)
            {
                mPairsProperty.DeleteArrayElementAtIndex(i--);
                continue;
            }

            if (!unUsedTypes.Remove(typeProperty.stringValue))
            {
                mPairsProperty.DeleteArrayElementAtIndex(i--);
                continue;
            }
            var type = Type.GetType(typeProperty.stringValue);
            EditorGUILayout.PropertyField(refProperty, new GUIContent(type.Name));
        }

        foreach (var unUsedType in unUsedTypes)
        {
            mPairsProperty.InsertArrayElementAtIndex(0);
            var elem = mPairsProperty.GetArrayElementAtIndex(0);
            var typeProperty = elem.FindPropertyRelative(nameof(PrefabDatabasePair.ObjectType));
            typeProperty.stringValue = unUsedType;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
