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
    public class FireArrowView : SkillView
    {
        public GameObject BulletPrefab;
        private GameObjectCache mCache;

        void Start()
        {
            mCache = new GameObjectCache(transform, BulletPrefab);
        }

        void Update()
        {
            var model = Model<FireArrow>();
            model.Update(TimeSpan.FromSeconds(Time.deltaTime));
        }
    }
}
