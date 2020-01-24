using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Skills
{
    public class Heal : Skill
    {
        public float Amount { get; } = 15;

        public Heal(Game game)
            : base(game)
        {
            Cooldown = TimeSpan.FromSeconds(20);
            Cost = 30;
        }

        protected override bool UseInner(Vector2 pointer)
        {
            if (Game.Player.Health >= Game.Player.MaxHealth.Value)
                return false;

            Game.Player.Health += Amount * Game.Player.SkillDamage.Value;
            return true;
        }

        public override void Update(TimeSpan deltaTime)
        {
        }
    }
}