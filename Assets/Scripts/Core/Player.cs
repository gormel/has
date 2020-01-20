using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.NPC;
using Assets.Scripts.Core.Skills;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Player : Character
    {
        private readonly Game mGame;

        public Parameter SkillDamage { get; private set; } = new Parameter(1);
        public int KillCount { get; private set; }
        public int NextLevelKillCount { get; private set; } = 5;
        public int SkillPoints { get; private set; }
        public float Mana { get; internal set; } = 100;
        public Parameter MaxMana { get; private set; } = new Parameter(100);
        public Parameter ManaRegen { get; private set; } = new Parameter(0.5f);
        public event EventHandler Destroyed;

        public List<Skill> KnownSkills { get; } = new List<Skill>();

        private Stopwatch mAttackCooldown = Stopwatch.StartNew();

        public Player(Game game, Vector2 spawnPoint)
        {
            mGame = game;
            Position = spawnPoint;

            KnownSkills.Add(mGame.AllSkills[0]);
        }

        public override void OnDestroy()
        {
            Destroyed?.Invoke(this, EventArgs.Empty);
        }

        public void SetMoveDirection(Vector2 direction)
        {
            Direction = direction;
        }

        public void Update(TimeSpan deltaTime)
        {
            var dir = Direction;
            if (dir != Vector2.zero)
            {
                dir.Normalize();
                var newPosition = Position + dir * Speed.Value * (float)deltaTime.TotalSeconds;
                var collided = mGame.Map.CheckCollision(new Rect(newPosition, Size));
                collided = mGame.CheckMonsterCollision(collided);
                Position = new Vector2(collided.xMin, collided.yMin);
            }

            Mana = Mathf.Min(Mana + ManaRegen.Value * (float)deltaTime.TotalSeconds, MaxMana.Value);
        }

        public bool LearnSkill(Skill skill)
        {
            if (SkillPoints <= 0)
                return false;

            if (KnownSkills.Contains(skill))
                return false;

            //check requarements

            SkillPoints--;
            KnownSkills.Add(skill);
            return true;
        }

        public bool AttackMonster(Monster m)
        {
            if (Vector2.Distance(Position, m.Position) > AttackRange.Value)
                return false;

            if (mAttackCooldown.Elapsed.TotalSeconds < 1 / AttackRate.Value)
                return false;

            mAttackCooldown = Stopwatch.StartNew();
            mGame.Attack(this, m);

            if (m.Health <= 0)
            {
                KillCount++;
                if (KillCount >= NextLevelKillCount)
                {
                    KillCount = 0;
                    SkillPoints++;
                    NextLevelKillCount = (int)(NextLevelKillCount * 1.5);
                }
            }

            return true;
        }
    }
}
