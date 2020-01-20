using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.NPC;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Core.Skills
{
    public class FireArrow : Skill
    {
        public class Bullet
        {
            private readonly FireArrow mSkill;
            private readonly Monster mTarget;
            public Vector2 Position { get; private set; }
            public Vector2 Direction { get; private set; }

            internal Bullet(FireArrow skill, Monster target)
            {
                mSkill = skill;
                mTarget = target;
                Position = mSkill.Game.Player.Bounds.center;
                Direction = (target.Position - Position).normalized;
            }

            internal bool Upadte(TimeSpan deltaTime)
            {
                Direction = (mTarget.Bounds.center - Position).normalized;
                Position += Direction * mSkill.ArrowSpeed * (float)deltaTime.TotalSeconds;


                for (int x = Math.Max(0, Mathf.FloorToInt(Position.x - 1)); x < Math.Min(Position.x + 2, mSkill.Game.Map.Width); x++)
                {
                    for (int y = Math.Max(0, Mathf.FloorToInt(Position.y - 1)); y < Math.Min(Position.y + 2, mSkill.Game.Map.Height); y++)
                    {
                        if (mSkill.Game.Map.StaticObjects[x, y] == null || mSkill.Game.Map.StaticObjects[x, y].IsWalkable)
                            continue;

                        if (new Rect(x, y, 1, 1).Contains(Position))
                            return true;
                    }
                }

                var monster = mSkill.Game.Monsters.FirstOrDefault(m => m.Bounds.Contains(Position));
                if (monster != null)
                {
                    mSkill.Game.TakeDamage(mSkill.Damage, monster);
                    return true;
                }

                return false;
            }
        }

        private Stopwatch mCooldownTimer = Stopwatch.StartNew();
        public float ArrowSpeed { get; }
        public float Damage { get; } = 50;
        public List<Bullet> Bullets { get; } = new List<Bullet>();

        public FireArrow(Game game)
            : base(game)
        {
            Cost = 10;
            Cooldown = TimeSpan.FromSeconds(5);
            ArrowSpeed = 6;
        }

        public override float CooldownPercent => Mathf.Min(1, (float)(mCooldownTimer.Elapsed.TotalSeconds / Cooldown.TotalSeconds));

        public override bool Use(Vector2 pointer)
        {
            var target = Game.Monsters.FirstOrDefault(m => m.Bounds.Contains(pointer));
            if (target == null)
                return false;

            if (mCooldownTimer.Elapsed < Cooldown)
                return false;

            if (Game.Player.Mana < Cost)
                return false;

            mCooldownTimer = Stopwatch.StartNew();
            Game.Player.Mana -= Cost;
            Bullets.Add(new Bullet(this, target));
            return true;
        }

        public override void Update(TimeSpan deltaTime)
        {
            Bullets.RemoveAll(b => b.Upadte(deltaTime));
        }
    }
}
