using Assets.Scripts.Core.Common;
using Assets.Scripts.Core.NPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Game
    {
        public Map Map { get; private set; }
        public Player Player { get; private set; }
        public List<Monster> Monsters { get; private set; } = new List<Monster>();//quad tree

        public Game()
        {
            Map = new Map();
            //generate map

            Player = new Player(this);
            for (int i = 0; i < 15; i++)
            {
                var x = UnityEngine.Random.Range(0, Map.Width);
                var y = UnityEngine.Random.Range(0, Map.Height);

                Monsters.Add(new Monster(this, new Vector2(x, y)));
            }
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

        public void Attack(Character source, Character target)
        {
            target.Health -= source.Attack.Value;
            if (target.Health <= 0)
                target.OnDestroy();
        }
    }
}
