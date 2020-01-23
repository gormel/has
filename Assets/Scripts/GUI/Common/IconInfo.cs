using System;
using UnityEngine;

namespace Assets.Scripts.GUI.Common
{
    [Serializable]
    public class IconInfo : ScriptableObject
    {
        public string SkillType;
        public Sprite Icon;
    }
}