using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.NPC;
using Assets.Scripts.Core.Skills;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Items;
using Assets.Scripts.Core.Items.Base;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Player : Character
    {
        private Game mGame;

        public Parameter SkillDamage { get; private set; } = new Parameter(1);
        public int KillCount { get; private set; }
        public int NextLevelKillCount { get; private set; } = 5;
        public int SkillPoints { get; private set; }
        public float Mana { get; internal set; } = 100;
        public Parameter MaxMana { get; private set; } = new Parameter(100);
        public Parameter ManaRegen { get; private set; } = new Parameter(0.5f);
        public event EventHandler Destroyed;

        public Inventory Inventory { get; }

        public List<SkillReference> KnownSkills { get; } = new List<SkillReference>();
        public SkillReference[] SelectedSkills { get; } = new SkillReference[4];

        private Stopwatch mAttackCooldown = Stopwatch.StartNew();

        private Player()
        {
            Inventory = new Inventory(this);
        }

        public static Player Create(Player saved, Game game, Vector2 spawnPoint)
        {
            if (saved == null)
                return Create(new Player(), game, spawnPoint);

            saved.mGame = game;
            saved.Position = spawnPoint;
            return saved;
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
            Health = Mathf.Min(Health + HealthRegen.Value * (float)deltaTime.TotalSeconds, MaxHealth.Value);
        }

        public bool LearnSkill(Skill skill)
        {
            if (SkillPoints <= 0)
                return false;

            if (KnownSkills.Any(r => r.Skill == skill))
                return false;

            if (!SkillRequarements.Check(skill, this))
                return false;

            SkillPoints--;
            KnownSkills.Add(GetSkillReference(skill));
            return true;
        }

        public SkillReference GetSkillReference(Skill skill)
        {
            return new SkillReference(skill, () => mGame.AllSkills);
        }

        public bool AttackMonster(Monster m)
        {
            if (Vector2.Distance(Position, m.Position) > AttackRange.Value)
                return false;

            if (mAttackCooldown.Elapsed.TotalSeconds < 1 / AttackRate.Value)
                return false;

            mAttackCooldown = Stopwatch.StartNew();
            mGame.Attack(this, m);
            
            return true;
        }

        public bool PickUp(Item item)
        {
            if (Inventory.Bag.Count >= Inventory.Size)
                return false;

            mGame.Items.Remove(item);
            Inventory.Bag.Add(item);
            return true;
        }

        public override void OnKill(Character target)
        {
            KillCount++;
            if (KillCount >= NextLevelKillCount)
            {
                KillCount = 0;
                SkillPoints++;
                NextLevelKillCount = (int)(NextLevelKillCount * 1.5);
            }
        }
    }
}
