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
        private ParameterStatus mArmorStatus;

        public Chainmail(int level)
        {
            var maxHp = Random.Range(10, 20) * (level + 1);
            var armor = Random.Range(1, 3) * (level + 1);

            mMaxHpStatus = new PermanentParameterStatus(ChangeType.Add, maxHp);
            mArmorStatus = new PermanentParameterStatus(ChangeType.Add, armor);

            PropertyDescriptions.Add($"Max health +{maxHp}");
            PropertyDescriptions.Add($"Armor +{armor}");
        }

        public override void OnPuton(Player player)
        {
            player.MaxHealth.Statuses.Add(mMaxHpStatus);
            player.Armor.Statuses.Add(mArmorStatus);
        }

        public override void OnPutOff(Player player)
        {
            player.MaxHealth.Statuses.Remove(mMaxHpStatus);
            player.HealthRegen.Statuses.Remove(mArmorStatus);
        }
    }
}
