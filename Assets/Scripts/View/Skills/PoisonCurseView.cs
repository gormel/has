using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Skills;
using UnityEngine;

namespace Assets.Scripts.View.Skills
{
    public class PoisonCurseView : SeparatedSkillView<PoisonCurse.Curse>
    {
        protected override void ApplyPatricle(PoisonCurse.Curse particle, GameObject target)
        {
            target.transform.localPosition = particle.Position;
        }

        protected override IEnumerable<PoisonCurse.Curse> GetParticles<TModel>(TModel model)
        {
            return (model as PoisonCurse)?.Curses;
        }
    }
}
