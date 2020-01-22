using Assets.Scripts.Core.Common.ParameterStatuses;
using Assets.Scripts.Core.Items.Base;
using UnityEngine;

namespace Assets.Scripts.Core.Items.Weapons
{
    public class Sword : Weapon
    {
        private readonly ParameterStatus mAttackStatus;

        public Sword(int level)
        {
            var attack = Random.Range(level / 2 + 1, level) * 5;
            mAttackStatus = new PermanentParameterStatus(ChangeType.Add, attack);

            PropertyDescriptions.Add($"Attack +{attack}");
        }

        public override void OnPuton(Player player)
        {
            player.Attack.Statuses.Add(mAttackStatus);
        }

        public override void OnPutOff(Player player)
        {
            player.Attack.Statuses.Remove(mAttackStatus);
        }
    }
}
