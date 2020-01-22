using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.View.Common;
using UnityEngine;

namespace Assets.Scripts.View.Skills {
    public abstract class SeparatedSkillView<TParticle> : SkillView
    {
        public GameObject ParticlePrefab;
        private GameObjectCache mCache;
        private CollectionObserver<TParticle> mParticlesObserver;
        private Dictionary<TParticle, GameObject> mParticleViews = new Dictionary<TParticle, GameObject>();

        protected virtual void Start()
        {
            mCache = new GameObjectCache(transform, ParticlePrefab);
        }

        public override void Load<T>(T model, Root root)
        {
            base.Load(model, root);

            if (mParticlesObserver != null)
            {
                mParticlesObserver.Added -= BulledsObserverOnAdded;
                mParticlesObserver.Removed -= BulledsObserverOnRemoved;
            }
            mParticlesObserver = new CollectionObserver<TParticle>(GetParticles(model));
            mParticlesObserver.Added += BulledsObserverOnAdded;
            mParticlesObserver.Removed += BulledsObserverOnRemoved;
        }

        private void BulledsObserverOnRemoved(object sender, CollectionChangedEventArgs<TParticle> e)
        {
            mCache.Release(mParticleViews[e.Elem]);
            mParticleViews.Remove(e.Elem);
        }

        private void BulledsObserverOnAdded(object sender, CollectionChangedEventArgs<TParticle> e)
        {
            var view = mCache.Allocate();
            mParticleViews[e.Elem] = view;
        }

        protected virtual void OnDestroy()
        {
            if (mParticlesObserver != null)
            {
                mParticlesObserver.Added -= BulledsObserverOnAdded;
                mParticlesObserver.Removed -= BulledsObserverOnRemoved;
            }
        }

        protected virtual void Update()
        {
            var model = Model<Skill>();
            model.Update(TimeSpan.FromSeconds(Time.deltaTime));
            mParticlesObserver.Update();

            foreach (var bullet in mParticleViews.Keys)
            {
                var view = mParticleViews[bullet];
                ApplyPatricle(bullet, view);
            }
        }

        protected abstract void ApplyPatricle(TParticle particle, GameObject target);
        protected abstract IEnumerable<TParticle> GetParticles<TModel>(TModel model);
    }
}