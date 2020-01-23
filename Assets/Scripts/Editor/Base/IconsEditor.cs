using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.GUI.Common;
using Assets.Scripts.GUI.Skill;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Assets.Scripts.Editor.Base
{
    public class IconsEditor<TModel> : UnityEditor.Editor
    {
        private IconsDatabase<TModel> mSkillIcons;
        Type[] mTypes;

        private void OnEnable()
        {
            mSkillIcons = serializedObject.targetObject as IconsDatabase<TModel>;
            mTypes = typeof(TModel).Assembly.GetTypes().Where(t => typeof(TModel).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface).ToArray();
        }


        public override void OnInspectorGUI()
        {
            var dict = mSkillIcons.Icons
                           ?.Where(i => !string.IsNullOrEmpty(i.SkillType))
                           ?.ToDictionary(t => t.SkillType, t => t.Icon) ?? new Dictionary<string, Sprite>();
            foreach (var type in mTypes)
            {
                var key = type.AssemblyQualifiedName;
                dict.TryGetValue(key, out var icon);
                dict[key] = EditorGUILayout.ObjectField(type.Name, icon, typeof(Sprite), false) as Sprite;
                if (icon != dict[key])
                    EditorSceneManager.MarkAllScenesDirty();
            }

            mSkillIcons.Icons = dict.Select(p => new IconInfo(){ Icon = p.Value, SkillType = p.Key }).ToList();
        }
    }
}
