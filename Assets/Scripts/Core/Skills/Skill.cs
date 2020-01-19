using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Skills
{
    public abstract class Skill
    {
        public float Cost { get; protected set; }
        public TimeSpan Cooldown { get; protected set; }

        public abstract bool Use(Game game, Vector2 pointer);
    }
}
