using UnityEditor;
using System;
using Assets.Scripts.View.NPC;
using Assets.Scripts.Core.NPC;

[CustomEditor(typeof(MonsterPrefabDatabase))]
public class MonsterObjectInfoEditor : ObjectInfoEditor
{
    protected override Type GetBaseObjectType()
    {
        return typeof(Monster);
    }
}