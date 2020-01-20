using Assets.Scripts.Core.NPC;
using Assets.Scripts.View.Common;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.View.NPC
{
    public class MonsterView : BaseView
    {
        public GameObject Highlight;

        Monster mMonster;

        public override void Load<T>(T model, Root root)
        {
            mMonster = model as Monster;
            mMonster.Destroyed += Monster_Destroyed;
            transform.localScale = mMonster.Size;
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

        void Update()
        {
            mMonster.Update(TimeSpan.FromSeconds(Time.deltaTime));

            transform.localPosition = mMonster.Position;
        }

        void OnMouseOver()
        {
            Highlight.SetActive(true);
        }

        void OnMouseExit()
        {
            Highlight.SetActive(false);
        }
    }
}
