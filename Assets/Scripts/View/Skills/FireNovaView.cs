using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.View.Common;
using UnityEngine;

namespace Assets.Scripts.View.Skills {
    public class FireNovaView : SeparatedSkillView<FireNova.Charge>
    {
        protected override void ApplyPatricle(FireNova.Charge particle, GameObject target)
        {
            target.transform.localPosition = particle.Center;
            target.transform.localScale = new Vector3(particle.Radius, particle.Radius, 1);
        }

        protected override IEnumerable<FireNova.Charge> GetParticles<TModel>(TModel model)
        {
            return (model as FireNova)?.Charges;
        }
    }
}