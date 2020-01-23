using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.Editor.Base;
using Assets.Scripts.GUI.Skill;
using Assets.Scripts.View.Common;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(SkillIcons))]
public class SkillIconsEditor : IconsEditor<Skill>
{
}