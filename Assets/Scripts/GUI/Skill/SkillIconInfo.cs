using System;
using UnityEngine;

namespace Assets.Scripts.GUI.Skill {
    [Serializable]
    public class SkillIconInfo : ScriptableObject
    {
        public string SkillType;
        public Sprite Icon;
    }
}