using System;
using Assets.Scripts.Core.Common.ParameterStatuses;
using UnityEngine;

namespace Assets.Scripts.Core.Skills
{
    public class SlowCurse : Skill
    {
        public TimeSpan Duration => TimeSpan.FromSeconds(10);
        public float Force { get; } = 0.5f;

        public SlowCurse(Game game)
            : base(game)
        {
            Cooldown = TimeSpan.FromSeconds(3);
            Cost = 10;
        }

        protected override bool UseInner(Vector2 pointer)
        {
            var target = Game.QueryMonster(pointer);
            if (target == null)
                return false;

            target.Speed.Statuses.Add(new TimeParameterStatus(ChangeType.Mul, Force * Game.Player.SkillDamage.Value, Duration));
            return true;
        }

        public override void Update(TimeSpan deltaTime)
        {
        }
    }
}
