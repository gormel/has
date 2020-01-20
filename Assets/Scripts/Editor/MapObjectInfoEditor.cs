using UnityEditor;
using Assets.Scripts.View.Common;
using System;
using Assets.Scripts.Core.Static;

[CustomEditor(typeof(MapObjectPrefabDatabase))]
public class MapObjectInfoEditor : ObjectInfoEditor
{
    protected override Type GetBaseObjectType()
    {
        return typeof(MapObject);
    }
}
