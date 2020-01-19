using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Skills
{
    public class FireArrow : Skill
    {
        public FireArrow()
        {
            Cost = 10;
            Cooldown = TimeSpan.FromSeconds(5);
        }

        public override bool Use(Game game, Vector2 pointer)
        {
            throw new NotImplementedException();
        }
    }
}
