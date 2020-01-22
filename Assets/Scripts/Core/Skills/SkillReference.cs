using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.Skills
{
    public class SkillReference
    {
        private readonly Func<IEnumerable<Skill>> mGetDatabase;
        public Skill Skill => mGetDatabase().FirstOrDefault(s => s.GetType().AssemblyQualifiedName == mSkillType);

        private string mSkillType;

        public SkillReference(Skill skill, Func<IEnumerable<Skill>> getDatabase)
        {
            mGetDatabase = getDatabase;
            mSkillType = skill.GetType().AssemblyQualifiedName;
        }
    }
}
