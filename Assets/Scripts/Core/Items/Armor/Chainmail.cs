using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Common.ParameterStatuses;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Core.Items.Armor
{
    public class Chainmail : Base.Armor
    {
        private ParameterStatus mMaxHpStatus;
        private ParameterStatus mHpRegenStatus;

        public Chainmail(int level)
        {
            var maxHp = Random.Range(10, 20) * (level + 1);
            var hpRegen = Random.Range(1, 3) * (level + 1);

            mMaxHpStatus = new PermanentParameterStatus(ChangeType.Add, maxHp);
            mHpRegenStatus = new PermanentParameterStatus(ChangeType.Add, hpRegen);

            PropertyDescriptions.Add($"Max health +{maxHp}");
            PropertyDescriptions.Add($"Health regen +{hpRegen}");
        }

        public override void OnPuton(Player player)
        {
            player.MaxHealth.Statuses.Add(mMaxHpStatus);
            player.HealthRegen.Statuses.Add(mHpRegenStatus);
        }

        public override void OnPutOff(Player player)
        {
            player.MaxHealth.Statuses.Remove(mMaxHpStatus);
            player.HealthRegen.Statuses.Remove(mHpRegenStatus);
        }
    }
}
