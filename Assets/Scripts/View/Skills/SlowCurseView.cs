using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.NPC;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.View.NPC;
using UnityEngine;

namespace Assets.Scripts.View.Skills
{
    public class SlowCurseView : SkillView
    {
        public GameObject EffectPrefab;

        public override void Use(Root root, Vector2 pointer)
        {
            if (Physics.Raycast(new Ray(new Vector3(pointer.x, pointer.y, Camera.main.transform.position.z), Vector3.forward), out var hit))
            {
                var monster = hit.collider.gameObject.GetComponent<MonsterView>();
                if (monster != null)
                {
                    var inst = Instantiate(EffectPrefab);
                    inst.transform.SetParent(transform);
                    var effect = inst.GetComponent<SlowCurseEffect>();
                    effect.Monster = monster.Model<Monster>();
                    effect.Duration = Model<SlowCurse>().Duration;
                }
            }
        }
    }
}
