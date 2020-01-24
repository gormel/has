using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Skills;
using UnityEngine;

namespace Assets.Scripts.View.Skills
{
    public class HasteView : SkillView
    {
        public GameObject EffectPrefab;

        public override void Use(Root root, Vector2 pointer)
        {
            var inst = Instantiate(EffectPrefab);
            inst.transform.SetParent(transform);
            var effect = inst.GetComponent<HasteEffect>();
            effect.Duration = Model<Haste>().Duration;
            effect.Player = root.PlayerView.Model<Player>();
        }
    }
}
