using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.Static
{
    public class Wall : MapObject
    {
        public override bool IsWalkable => false;
        public override bool IsInteractive => false;
    }
}
