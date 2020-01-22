using System;
using Assets.Scripts.Core.Items.Base;
using Assets.Scripts.Core.NPC;
using Assets.Scripts.View.Items;
using UnityEditor;

[CustomEditor(typeof(ItemPrefabDatabase))]
public class ItemObjectInfoEditor : ObjectInfoEditor
{
    protected override Type GetBaseObjectType()
    {
        return typeof(Item);
    }
}