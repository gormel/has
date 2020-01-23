using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.GUI.Common
{
    public class IconsDatabase<TModel> : MonoBehaviour
    {
        public List<IconInfo> Icons;

        public Sprite GetIcon(TModel model)
        {
            if (model == null)
                return null;

            return Icons.FirstOrDefault(i => i.SkillType == model.GetType().AssemblyQualifiedName)?.Icon;
        }
    }
}
