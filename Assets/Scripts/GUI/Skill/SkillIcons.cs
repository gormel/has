using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GUI.Skill
{
    public class SkillIcons : MonoBehaviour
    {
        public List<SkillIconInfo> Icons;

        public Sprite GetIcon(Core.Skills.Skill skill)
        {
            if (skill == null)
                return null;

            return Icons.FirstOrDefault(i => i.SkillType == skill.GetType().AssemblyQualifiedName)?.Icon;
        }
    }
}
