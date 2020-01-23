using UnityEngine;

namespace Assets.Scripts.Core.Common
{
    public abstract class Character
    {
        public Vector2 Size => Vector2.one * 0.8f;
        public Rect Bounds => new Rect(Position, Size);
        public Vector2 Position { get; protected set; }
        public Parameter Speed { get; } = new Parameter(1.5f);//unit per sec
        public Vector2 Direction { get; protected set; }

        public float Health { get; set; } = 100;
        public Parameter MaxHealth { get; } = new Parameter(100);
        public Parameter HealthRegen { get; } = new Parameter(0);

        public Parameter Armor { get; } = new Parameter(0);

        public Parameter Attack { get; } = new Parameter(20);
        public Parameter AttackRange { get; } = new Parameter(1.45f);
        public Parameter AttackRate { get; } = new Parameter(1);//hit per sec

        public abstract void OnDestroy();
        public virtual void OnKill(Character target) { }
    }
}
