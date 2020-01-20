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
        private CollectionObserver<FireArrow.Bullet> mBulledsObserver;
        private Dictionary<FireArrow.Bullet, GameObject> mBulletViews = new Dictionary<FireArrow.Bullet, GameObject>();

        void Start()
        {
            mCache = new GameObjectCache(transform, BulletPrefab);
        }

        public override void Load<T>(T model, Root root)
        {
            base.Load(model, root);

            if (model is FireArrow)
            {
                if (mBulledsObserver != null)
                {
                    mBulledsObserver.Added -= BulledsObserverOnAdded;
                    mBulledsObserver.Removed -= BulledsObserverOnRemoved;
                }
                mBulledsObserver = new CollectionObserver<FireArrow.Bullet>((model as FireArrow).Bullets);
                mBulledsObserver.Added += BulledsObserverOnAdded;
                mBulledsObserver.Removed += BulledsObserverOnRemoved;
            }
        }

        private void BulledsObserverOnRemoved(object sender, CollectionChangedEventArgs<FireArrow.Bullet> e)
        {
            mCache.Release(mBulletViews[e.Elem]);
            mBulletViews.Remove(e.Elem);
        }

        private void BulledsObserverOnAdded(object sender, CollectionChangedEventArgs<FireArrow.Bullet> e)
        {
            var view = mCache.Allocate();
            mBulletViews[e.Elem] = view;
        }

        void OnDestroy()
        {
            if (mBulledsObserver != null)
            {
                mBulledsObserver.Added -= BulledsObserverOnAdded;
                mBulledsObserver.Removed -= BulledsObserverOnRemoved;
            }
        }

        void Update()
        {
            var model = Model<FireArrow>();
            model.Update(TimeSpan.FromSeconds(Time.deltaTime));
            mBulledsObserver.Update();

            foreach (var bullet in mBulletViews.Keys)
            {
                var view = mBulletViews[bullet];
                view.transform.localPosition = bullet.Position;
                var angle = Mathf.Atan2(bullet.Direction.y, bullet.Direction.x);
                view.transform.eulerAngles = new Vector3(0, 0, angle * Mathf.Rad2Deg);
            }
        }
    }
}
