using System;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.View.NPC;
using Assets.Scripts.View.Skills;
using UnityEditor;

[CustomEditor(typeof(SkillPrefabDatabase))]
public class SkillObjectInfoEditor : ObjectInfoEditor
{
    protected override Type GetBaseObjectType()
    {
        return typeof(Skill);
    }
}