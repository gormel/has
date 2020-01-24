using System;
using Assets.Scripts.Core;
using Assets.Scripts.View.Common;
using UnityEngine;

namespace Assets.Scripts.View.Skills
{
    public class HasteEffect : MonoBehaviour
    {
        public TimeSpan Duration;
        public Player Player;

        public RemoveAfterSeconds Remover;

        void Start()
        {
            Remover.enabled = false;
            Remover.Seconds = (float)Duration.TotalSeconds;
            Remover.enabled = true;
        }

        void Update()
        {
            transform.localPosition = Player.Bounds.center;
        }
    }
}