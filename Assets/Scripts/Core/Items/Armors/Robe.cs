using System;
using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.Common.ParameterStatuses;
using Assets.Scripts.Core.Items.Base;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Core.Items.Armors
{
    public class Robe : Armor
    {
        private float mMaxMana;
        private float mManaRegen;

        public Robe(int level)
        {
            mMaxMana = Random.Range(10, 20) * (level + 1);
            mManaRegen = Random.Range(0.5f, 0.7f) * (level + 1);

            PropertyDescriptions.Add("==Robe==");
            PropertyDescriptions.Add($"Max mana +{mMaxMana}");
            PropertyDescriptions.Add($"Mana regen +{mManaRegen}");
        }

        protected override (ParameterStatus Status, Func<Player, Parameter> Param)[] Parameters()
        {
            return new (ParameterStatus, Func<Player, Parameter>)[]
            {
                (new PermanentParameterStatus(ChangeType.Add, mMaxMana), p => p.MaxMana),
                (new PermanentParameterStatus(ChangeType.Add, mManaRegen), p => p.ManaRegen),
            };
        }
    }
}