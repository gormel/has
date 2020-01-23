using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Items.Base;
using Assets.Scripts.Editor.Base;
using Assets.Scripts.GUI.Inventory;
using Assets.Scripts.GUI.Skill;
using UnityEditor;

namespace Assets.Scripts.Editor
{
    [CustomEditor(typeof(ItemIcons))]
    public class ItemIconsEditor : IconsEditor<Item>
    {
    }
}
