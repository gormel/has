using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.GUI.Skill;
using Assets.Scripts.View.Common;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(SkillIcons))]
public class SkillIconsEditor : Editor
{
    private SkillIcons mSkillIcons;
    Type[] mTypes;

    private void OnEnable()
    {
        mSkillIcons = serializedObject.targetObject as SkillIcons;
        mTypes = typeof(Skill).Assembly.GetTypes().Where(t => typeof(Skill).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface).ToArray();
    }

    public override void OnInspectorGUI()
    {
        var dict = mSkillIcons.Icons?.ToDictionary(t => t.SkillType, t => t.Icon) ?? new Dictionary<string, Sprite>();
        foreach (var type in mTypes)
        {
            var key = type.AssemblyQualifiedName;
            dict.TryGetValue(key, out var icon);
            dict[key] = EditorGUILayout.ObjectField(type.Name, icon, typeof(Sprite), false) as Sprite;
            if (icon != dict[key])
                EditorSceneManager.MarkAllScenesDirty();
        }

        mSkillIcons.Icons = dict.Select(p => new SkillIconInfo(){ Icon = p.Value, SkillType = p.Key }).ToList();
    }
}