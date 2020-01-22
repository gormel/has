using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.View.Common;
using UnityEngine;

namespace Assets.Scripts.View.Skills
{
    public class FireArrowView : SeparatedSkillView<FireArrow.Bullet>
    {
        protected override void ApplyPatricle(FireArrow.Bullet particle, GameObject target)
        {
            target.transform.localPosition = particle.Position;
            var angle = Mathf.Atan2(particle.Direction.y, particle.Direction.x);
            target.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
        }

        protected override IEnumerable<FireArrow.Bullet> GetParticles<TModel>(TModel model)
        {
            return (model as FireArrow)?.Bullets;
        }
    }
}
