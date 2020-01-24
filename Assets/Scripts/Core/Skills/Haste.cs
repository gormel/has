using System;
using Assets.Scripts.Core.Common.ParameterStatuses;
using UnityEngine;

namespace Assets.Scripts.Core.Skills
{
    public class Haste : Skill
    {
        public TimeSpan Duration { get; } = TimeSpan.FromSeconds(10);
        public float Force { get; } = 1.5f;

        public Haste(Game game)
            : base(game)
        {
            Cooldown = TimeSpan.FromSeconds(15);
            Cost = 10;
        }

        protected override bool UseInner(Vector2 pointer)
        {
            Game.Player.Speed.Statuses.Add(new TimeParameterStatus(ChangeType.Mul, Force * Game.Player.SkillDamage.Value, Duration));
            return true;
        }

        public override void Update(TimeSpan deltaTime)
        {
        }
    }
}
