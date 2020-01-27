using System;
using UnityEngine;

namespace Assets.Scripts.Core.Skills
{
    public class FireBlast : Skill
    {
        public float Radius { get; } = 2;
        public float Damage { get; } = 60;
        public FireBlast(Game game)
            : base(game)
        {
            Cooldown = TimeSpan.FromSeconds(7);
            Cost = 25;
        }

        protected override bool UseInner(Vector2 pointer)
        {
            var so = Game.Map.StaticObjects[(int)(pointer.x + 0.5), (int)(pointer.y + 0.5)];
            if (so != null && !so.IsWalkable)
                return false;

            var monsters = Game.QueryMonsters(pointer, Radius);
            foreach (var monster in monsters)
                Game.TakeDamage(Game.Player, Damage * Game.Player.SkillDamage.Value, monster);

            return true;
        }

        public override void Update(TimeSpan deltaTime)
        {
        }
    }
}