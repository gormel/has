using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.NPC;
using UnityEngine;

namespace Assets.Scripts.Core.Skills
{
    public class FireNova : Skill
    {
        public class Charge
        {
            private readonly FireNova mNova;

            public float Radius { get; private set; } = 0f;
            public Vector2 Center { get; }

            private List<Monster> mDamaged = new List<Monster>();

            public Charge(FireNova nova, Vector2 center)
            {
                mNova = nova;
                Center = center;
            }

            internal bool Update(TimeSpan deltaTime)
            {
                Radius += mNova.ExpansionSpeed * (float)deltaTime.TotalSeconds;
                var monstersIn = mNova.Game.Monsters.Where(m => MathUtils.DistanceToRect(Center, m.Bounds) <= Radius).Except(mDamaged).ToList();
                foreach (var monster in monstersIn)
                {
                    mNova.Game.TakeDamage(mNova.Game.Player, mNova.Damage * mNova.Game.Player.SkillDamage.Value, monster);
                    mDamaged.Add(monster);
                }

                return Radius >= mNova.MaxRadius;
            }
        }

        public float MaxRadius { get; } = 3;
        public float ExpansionSpeed { get; } = 6;
        public float Damage { get; } = 30;

        public List<Charge> Charges { get; } = new List<Charge>();

        public FireNova(Game game)
            : base(game)
        {
            Cost = 20;
            Cooldown = TimeSpan.FromSeconds(20);
        }

        protected override bool UseInner(Vector2 pointer)
        {
            Charges.Add(new Charge(this, Game.Player.Bounds.center));
            return true;
        }

        public override void Update(TimeSpan deltaTime)
        {
            Charges.RemoveAll(b => b.Update(deltaTime));
        }
    }
}
