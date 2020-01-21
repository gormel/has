using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Skills;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Game
    {
        public Map Map { get; private set; }
        public Player Player { get; private set; }
        public List<Monster> Monsters { get; private set; } = new List<Monster>();//quad tree
        public List<Skill> AllSkills { get; private set; } = new List<Skill>();

        public Game()
        {
            Map = new Map();
            //generate map

            AllSkills.Add(new FireArrow(this));//auto fill by types

            Player = new Player(this, Map.SpawnPoint);

            for (int i = 0; i < 15; i++)
                Monsters.Add(new Monster(this, Map.GetRandomFreeLocation()));
        }

        public Rect CheckMonsterCollision(Rect source, params Monster[] skip)
        {
            foreach (var m in Monsters)
            {
                if (skip.Contains(m))
                    continue;

                if (m.Bounds.Overlaps(source))
                    source = MathUtils.Collide(source, m.Bounds);
            }

            return source;
        }

        public void TakeDamage(float damage, Character target)
        {
            target.Health -= damage;
            if (target.Health <= 0)
                target.OnDestroy();
        }

        public void Attack(Character source, Character target)
        {
            TakeDamage(source.Attack.Value, target);
        }
    }
}
