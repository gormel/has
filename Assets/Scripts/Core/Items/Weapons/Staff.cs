using System;
using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.Common.ParameterStatuses;
using Assets.Scripts.Core.Items.Base;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Core.Items.Weapons
{
    public class Staff : Weapon
    {
        private float mSpellDamage;

        public Staff(int level)
        {
            mSpellDamage = Random.Range(1, 1.2f) * (level + 1);

            PropertyDescriptions.Add("==Staff==");
            PropertyDescriptions.Add($"Skill damage x{mSpellDamage}");
        }

        protected override (ParameterStatus Status, Func<Player, Parameter> Param)[] Parameters()
        {
            return new (ParameterStatus, Func<Player, Parameter>)[]
            {
                (new PermanentParameterStatus(ChangeType.Mul, mSpellDamage), p => p.SkillDamage)
            };
        }
    }
}