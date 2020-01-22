using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
            private Monster mTarget;
            public Vector2 Position { get; private set; }
            public Vector2 Direction { get; private set; }

            internal Bullet(FireArrow skill, Monster target)
            {
                mSkill = skill;
                mTarget = target;
                Position = mSkill.Game.Player.Bounds.center;
                Direction = (target.Position - Position).normalized;
                target.Destroyed += TargetOnDestroyed;
            }

            private void TargetOnDestroyed(object sender, EventArgs e)
            {
                mTarget.Destroyed -= TargetOnDestroyed;
                mTarget = null;
            }

            internal bool Update(TimeSpan deltaTime)
            {
                if (mTarget == null)
                    return true;

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
                    mSkill.Game.TakeDamage(mSkill.Game.Player, mSkill.Damage * mSkill.Game.Player.SkillDamage.Value, monster);
                    return true;
                }

                return false;
            }
        }

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

        protected override bool UseInner(Vector2 pointer)
        {
            var target = Game.QueryMonster(pointer);
            if (target == null)
                return false;

            Bullets.Add(new Bullet(this, target));
            return true;
        }

        public override void Update(TimeSpan deltaTime)
        {
            Bullets.RemoveAll(b => b.Update(deltaTime));
        }
    }
}
