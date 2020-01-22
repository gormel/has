using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.Common.ParameterStatuses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Items.Armor;
using Assets.Scripts.Core.Items.Base;
using Assets.Scripts.Core.Items.Weapons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Core.NPC
{
    public class Monster : Character
    {
        public virtual float DetectionRange { get; protected set; } = 5;

        private Game mGame;
        private readonly int mLevel;
        private Stopwatch mTurnCooldown = Stopwatch.StartNew();
        private Stopwatch mAttackCooldown = Stopwatch.StartNew();

        public event EventHandler Destroyed;
        public event EventHandler Attacked;

        public Monster(Game game, Vector2 position, int level)//level -> difficulty
        {
            mGame = game;
            mLevel = level;
            Position = position;

            Attack.Statuses.Add(new PermanentParameterStatus(ChangeType.Set, 3.4f + level));
            MaxHealth.Statuses.Add(new PermanentParameterStatus(ChangeType.Set, 60 + level * 10));
            Health = MaxHealth.Value;
        }

        public void Update(TimeSpan deltaTime)
        {
            var playerDistance = Vector2.Distance(mGame.Player.Position, Position);
            if (playerDistance > DetectionRange)
            {
                if (mTurnCooldown.Elapsed.TotalSeconds > 5)
                {
                    var angle = UnityEngine.Random.Range(0, Mathf.PI * 2);
                    Direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    mTurnCooldown = Stopwatch.StartNew();
                }
            }
            else
            {
                if (playerDistance > AttackRange.Value)
                {
                    Direction = mGame.Player.Position - Position;
                    Direction.Normalize();
                }
                else
                {
                    Direction = Vector2.zero;
                    if (mAttackCooldown.Elapsed.TotalSeconds >= 1 / AttackRate.Value)
                    {
                        mGame.Attack(this, mGame.Player);
                        mAttackCooldown = Stopwatch.StartNew();
                        Attacked?.Invoke(this, EventArgs.Empty);
                    }
                }

            }

            var dir = Direction;
            if (dir != Vector2.zero)
            {
                dir.Normalize();
                var newPos = Position + dir * Speed.Value * (float)deltaTime.TotalSeconds;
                var collided = mGame.CheckMonsterCollision(new Rect(newPos, Size), this);
                collided = MathUtils.Collide(collided, mGame.Player.Bounds);
                collided = mGame.Map.CheckCollision(collided);
                Position = new Vector2(collided.xMin, collided.yMin);
            }
        }

        public override void OnDestroy()
        {
            Destroyed?.Invoke(this, EventArgs.Empty);
            mGame.Monsters.Remove(this);
            //item pool
            if (Random.value < 0.33)
            {
                Item loot;
                if (Random.value < 0.5)
                    loot = new Chainmail(mLevel);
                else
                    loot = new Sword(mLevel);

                mGame.Items[loot] = Bounds.center;
            }
        }
    }
}
