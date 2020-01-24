using System;
using Assets.Scripts.Core.NPC;
using Assets.Scripts.View.Common;
using UnityEngine;

namespace Assets.Scripts.View.Skills {
    public class SlowCurseEffect : MonoBehaviour
    {
        public Monster Monster;
        public TimeSpan Duration;

        public RemoveAfterSeconds Remover;

        void Start()
        {
            Remover.enabled = false;
            Remover.Seconds = (float)Duration.TotalSeconds;
            Remover.enabled = true;

            Monster.Destroyed += MonsterOnDestroyed;
        }

        private void MonsterOnDestroyed(object sender, EventArgs e)
        {
            Monster.Destroyed -= MonsterOnDestroyed;
            if (isActiveAndEnabled)
                Destroy(gameObject);
        }

        void Update()
        {
            transform.localPosition = Monster.Bounds.center;
        }
    }
}