using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.View.Common;
using UnityEngine;

namespace Assets.Scripts.View.Skills {
    public class FireNovaView : SkillView
    {
        public GameObject ChargePrefab;
        private GameObjectCache mCache;
        private CollectionObserver<FireNova.Charge> mChargesObserver;
        private Dictionary<FireNova.Charge, GameObject> mChargeViews = new Dictionary<FireNova.Charge, GameObject>();
        void Start()
        {
            mCache = new GameObjectCache(transform, ChargePrefab);
        }

        public override void Load<T>(T model, Root root)
        {
            base.Load(model, root);

            if (model is FireNova)
            {
                if (mChargesObserver != null)
                {
                    mChargesObserver.Added -= BulledsObserverOnAdded;
                    mChargesObserver.Removed -= BulledsObserverOnRemoved;
                }
                mChargesObserver = new CollectionObserver<FireNova.Charge>((model as FireNova).Charges);
                mChargesObserver.Added += BulledsObserverOnAdded;
                mChargesObserver.Removed += BulledsObserverOnRemoved;
            }
        }

        private void BulledsObserverOnRemoved(object sender, CollectionChangedEventArgs<FireNova.Charge> e)
        {
            mCache.Release(mChargeViews[e.Elem]);
            mChargeViews.Remove(e.Elem);
        }

        private void BulledsObserverOnAdded(object sender, CollectionChangedEventArgs<FireNova.Charge> e)
        {
            var view = mCache.Allocate();
            mChargeViews[e.Elem] = view;
        }

        void OnDestroy()
        {
            if (mChargesObserver != null)
            {
                mChargesObserver.Added -= BulledsObserverOnAdded;
                mChargesObserver.Removed -= BulledsObserverOnRemoved;
            }
        }

        void Update()
        {
            var model = Model<FireNova>();
            model.Update(TimeSpan.FromSeconds(Time.deltaTime));
            mChargesObserver.Update();

            foreach (var bullet in mChargeViews.Keys)
            {
                var view = mChargeViews[bullet];
                view.transform.localPosition = bullet.Center;
                view.transform.localScale = new Vector3(bullet.Radius, bullet.Radius, 1);
            }
        }
    }
}