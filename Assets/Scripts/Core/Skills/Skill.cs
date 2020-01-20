using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Skills
{
    public abstract class Skill
    {
        protected Game Game { get; }
        public float Cost { get; protected set; }
        public TimeSpan Cooldown { get; protected set; }

        public abstract float CooldownPercent { get; }

        public Skill(Game game)
        {
            Game = game;
        }

        public abstract bool Use(Vector2 pointer);

        public abstract void Update(TimeSpan deltaTime);
    }
}
