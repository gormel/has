using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public float CooldownPercent => Mathf.Min(1, (float)(CooldownTimer.TotalSeconds / Cooldown.TotalSeconds));

        private Stopwatch mCooldownTimer = null;
        private TimeSpan CooldownTimer => mCooldownTimer?.Elapsed ?? Cooldown;

        public Skill(Game game)
        {
            Game = game;
        }

        public bool Use(Vector2 pointer)
        {
            if (CooldownTimer < Cooldown)
                return false;

            if (Game.Player.Mana < Cost)
                return false;

            if (UseInner(pointer))
            {
                Game.Player.Mana -= Cost;
                mCooldownTimer = Stopwatch.StartNew();
                return true;
            }

            return false;
        }

        protected abstract bool UseInner(Vector2 pointer);

        public abstract void Update(TimeSpan deltaTime);
    }
}
