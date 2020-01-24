using Assets.Scripts.Core.Skills;
using UnityEngine;

namespace Assets.Scripts.View.Skills
{
    public class FireBlastView : SkillView
    {
        public GameObject ExplosionPrefab;

        public override void Use(Root root, Vector2 pointer)
        {
            var inst = Instantiate(ExplosionPrefab);
            inst.transform.SetParent(transform);
            inst.transform.localPosition = pointer;
        }
    }
}