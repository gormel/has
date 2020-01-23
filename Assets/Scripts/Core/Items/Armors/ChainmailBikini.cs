using System;
using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.Common.ParameterStatuses;
using Assets.Scripts.Core.Items.Base;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Core.Items.Armors
{
    public class ChainmailBikini : Armor
    {
        private int mArmor;

        public ChainmailBikini(int level)
        {
            mArmor = 20 * (level + 1);

            PropertyDescriptions.Add("==Chainmail bikini==");
            PropertyDescriptions.Add($"Armor +{mArmor}");
        }

        protected override (ParameterStatus Status, Func<Player, Parameter> Param)[] Parameters()
        {
            return new (ParameterStatus, Func<Player, Parameter>)[]
            {
                (new PermanentParameterStatus(ChangeType.Add, mArmor), p => p.Armor),
            };
        }
    }
}