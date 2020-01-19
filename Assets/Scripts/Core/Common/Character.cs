﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core.Common
{
    public abstract class Character
    {
        public Vector2 Size => Vector2.one * 0.8f;
        public Rect Bounds => new Rect(Position, Size);
        public virtual Vector2 Position { get; protected set; }
        public Parameter Speed { get; } = new Parameter(1.5f);//unit per sec
        public virtual Vector2 Direction { get; protected set; }

        public float Health { get; set; } = 100;
        public Parameter MaxHealth { get; } = new Parameter(100);

        public Parameter Attack { get; } = new Parameter(20);
        public Parameter AttackRange { get; } = new Parameter(1.45f);
        public Parameter AttackRate { get; } = new Parameter(1);//hit per sec

        public abstract void OnDestroy();
    }
}
