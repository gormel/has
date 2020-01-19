using Assets.Scripts.Core.NPC;
using Assets.Scripts.View.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.View.NPC
{
    public class MonsterView : BaseView
    {
        Monster mMonster;

        public override void Load<T>(T model, Root root)
        {
            mMonster = model as Monster;
            mMonster.Destroyed += Monster_Destroyed;
        }

        public override T Model<T>()
        {
            return mMonster as T;
        }

        private void Monster_Destroyed(object sender, EventArgs e)
        {
            mMonster.Destroyed -= Monster_Destroyed;
            Destroy(gameObject);
        }

        private void Update()
        {
            mMonster.Update(TimeSpan.FromSeconds(Time.deltaTime));

            transform.localPosition = mMonster.Position;
        }
    }
}
