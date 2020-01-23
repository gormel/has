using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.Common.ParameterStatuses;
using Assets.Scripts.Core.Items.Base;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Core.Items.Armors
{
    public class Chainmail : Armor
    {
        private int mMaxHp;
        private int mArmor;

        public Chainmail(int level)
        {
            mMaxHp = Random.Range(10, 20) * (level + 1);
            mArmor = Random.Range(1, 3) * (level + 1);

            PropertyDescriptions.Add("==Chainmail==");
            PropertyDescriptions.Add($"Max health +{mMaxHp}");
            PropertyDescriptions.Add($"Armor +{mArmor}");
        }

        protected override (ParameterStatus Status, Func<Player, Parameter> Param)[] Parameters()
        {
            return new (ParameterStatus, Func<Player, Parameter>)[]
            {
                (new PermanentParameterStatus(ChangeType.Add, mMaxHp), p => p.MaxHealth),
                (new PermanentParameterStatus(ChangeType.Add, mArmor), p => p.Armor),
            };
        }
    }
}