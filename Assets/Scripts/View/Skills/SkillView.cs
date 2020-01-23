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
    public class SkillView : BaseView
    {
        private Skill mSkill;
        public override void Load<T>(T model, Root root)
        {
            mSkill = model as Skill;
        }

        public override T Model<T>()
        {
            return mSkill as T;
        }

        public virtual void Use(Vector2 pointer) { }
    }
}
