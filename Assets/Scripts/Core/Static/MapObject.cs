using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Common;

namespace Assets.Scripts.Core.Static
{
    public abstract class MapObject
    {
        public abstract bool IsWalkable { get; }
        public abstract bool IsInteractive { get; }

        public virtual void InteractFrom(Player palyer) { }
    }
}
