using System;
using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.Common.ParameterStatuses;
using Assets.Scripts.Core.Items.Base;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Core.Items.Weapons
{
    public class Sword : Weapon
    {
        private int mAttack;

        public Sword(int level)
        {
            mAttack = Random.Range(15, 20) * (level + 1);

            PropertyDescriptions.Add("==Sword==");
            PropertyDescriptions.Add($"Attack +{mAttack}");
        }

        protected override (ParameterStatus Status, Func<Player, Parameter> Param)[] Parameters()
        {
            return new (ParameterStatus, Func<Player, Parameter>)[]
            {
                (new PermanentParameterStatus(ChangeType.Add, mAttack), p => p.Attack)
            };
        }
    }
}
