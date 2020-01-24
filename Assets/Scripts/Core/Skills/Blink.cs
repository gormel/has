using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Skills
{
    public class Blink : Skill
    {
        public Blink(Game game)
            : base(game)
        {
            Cooldown = TimeSpan.FromSeconds(7);
            Cost = 20;
        }

        protected override bool UseInner(Vector2 pointer)
        {
            if (!Game.Map.IsFree(pointer))
                return false;

            Game.Player.SetPosition(pointer - Game.Player.Size / 2);
            return true;
        }

        public override void Update(TimeSpan deltaTime)
        {
        }
    }
}
