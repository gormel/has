using System;
using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.Common.ParameterStatuses;
using Assets.Scripts.Core.Items.Base;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Core.Items.Weapons
{
    public class Spear : Weapon
    {
        private int mAttack;
        private float mRange;

        public Spear(int level)
        {
            mAttack = Random.Range(3, 7) * (level + 1);
            mRange = 1.5f;

            PropertyDescriptions.Add("==Spear==");
            PropertyDescriptions.Add($"Attack +{mAttack}");
            PropertyDescriptions.Add($"Attack range +{mRange}");
        }

        protected override (ParameterStatus Status, Func<Player, Parameter> Param)[] Parameters()
        {
            return new (ParameterStatus, Func<Player, Parameter>)[]
            {
                (new PermanentParameterStatus(ChangeType.Add, mAttack), p => p.Attack),
                (new PermanentParameterStatus(ChangeType.Add, mRange), p => p.AttackRange),
            };
        }
    }
}