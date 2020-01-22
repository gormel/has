using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.Skills
{
    static class SkillRequarements
    {
        private abstract class Requarement
        {
            public abstract bool Check(Player player);
        }

        private class LearnedSkillRequarement : Requarement
        {
            private readonly Type mRequaeredSkillType;

            public LearnedSkillRequarement(Type requaeredSkillType)
            {
                mRequaeredSkillType = requaeredSkillType;
            }

            public override bool Check(Player player)
            {
                return player.KnownSkills.Any(s => s.Skill.GetType() == mRequaeredSkillType);
            }
        }

        private static Dictionary<Type, Requarement[]> mRequarements = new Dictionary<Type, Requarement[]>
        {
            { typeof(FireNova), new [] { new LearnedSkillRequarement(typeof(FireArrow)) } }
        };

        public static bool Check(Skill skill, Player player)
        {
            if (mRequarements.TryGetValue(skill.GetType(), out var reqs))
                return reqs.All(r => r.Check(player));

            return true;
        }
    }
}
