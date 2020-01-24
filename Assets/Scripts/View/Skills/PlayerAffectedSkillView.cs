using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core;
using UnityEngine;

namespace Assets.Scripts.View.Skills
{
    public class PlayerAffectedSkillView : SkillView
    {
        public GameObject EffectPrefab;

        public override void Use(Root root, Vector2 pointer)
        {
            var inst = Instantiate(EffectPrefab);
            inst.transform.SetParent(transform);
            inst.transform.localPosition = root.PlayerView.Model<Player>().Bounds.center;
        }
    }
}
