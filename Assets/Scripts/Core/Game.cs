using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Items;
using Assets.Scripts.Core.Items.Base;
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
        public Dictionary<Item, Vector2> Items { get; private set; } = new Dictionary<Item, Vector2>();

        public event EventHandler LevelComplete;

        public Game(Player saved, int level)
        {
            Map = new Map(this, level);

            foreach (var type in typeof(Skill).Assembly.GetTypes().Where(t => typeof(Skill).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface))
                AllSkills.Add((Skill)Activator.CreateInstance(type, this));

            Player = Player.Create(saved, this, Map.SpawnPoint);

            for (int i = 0; i < 5 + level * 3; i++)
                Monsters.Add(new Monster(this, Map.GetRandomFreeLocation(), level));
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

        public void TakeDamage(Character source, float damage, Character target)
        {
            target.Health -= damage;
            if (target.Health <= 0)
            {
                target.OnDestroy();
                source.OnKill(target);
            }
        }

        public Monster QueryMonster(Vector2 point)
        {
            return Monsters.FirstOrDefault(m => m.Bounds.Contains(point));
        }

        public void Attack(Character source, Character target)
        {
            TakeDamage(source, source.Attack.Value, target);
        }

        public void CompleteLevel()
        {
            LevelComplete?.Invoke(this, EventArgs.Empty);
        }
    }
}
