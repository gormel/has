using System;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts.Core.NPC;
using UnityEngine;

namespace Assets.Scripts.Core.Skills
{
    public class PoisonCurse : Skill
    {
        public class Curse
        {
            private Monster mTarget;
            private readonly PoisonCurse mCurse;
            private Stopwatch mTimer = Stopwatch.StartNew();

            public Vector2 Position {get; private set; }

            public Curse(Monster target, PoisonCurse curse)
            {
                mTarget = target;
                mCurse = curse;
                mTarget.Destroyed += TargetOnDestroyed;
            }

            private void TargetOnDestroyed(object sender, EventArgs e)
            {
                mTarget.Destroyed -= TargetOnDestroyed;
                mTarget = null;
            }

            public bool Update(TimeSpan deltaTime)
            {
                if (mTarget == null)
                    return true;

                if (mTimer.Elapsed >= mCurse.Duration)
                    return true;

                mCurse.Game.TakeDamage(mCurse.Game.Player, mCurse.DPS * (float)deltaTime.TotalSeconds * mCurse.Game.Player.SkillDamage.Value, mTarget);
                Position = mTarget.Bounds.center;
                return false;
            }
        }

        public List<Curse> Curses { get; } = new List<Curse>();
        public TimeSpan Duration { get; } = TimeSpan.FromSeconds(5);
        public float DPS { get; } = 15;

        public PoisonCurse(Game game)
            : base(game)
        {
            Cooldown = TimeSpan.FromSeconds(6);
            Cost = 15;
        }

        protected override bool UseInner(Vector2 pointer)
        {
            var monster = Game.QueryMonster(pointer);
            if (monster == null)
                return false;

            Curses.Add(new Curse(monster, this));
            return true;
        }

        public override void Update(TimeSpan deltaTime)
        {
            Curses.RemoveAll(c => c.Update(deltaTime));
        }
    }
}