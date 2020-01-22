using System.Collections.Generic;

namespace Assets.Scripts.Core.Items.Base
{
    public abstract class Item
    {
        public List<string> PropertyDescriptions { get; } = new List<string>();

        public abstract void OnPuton(Player player);
        public abstract void OnPutOff(Player player);
    }
}
